using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtmApi.Services.HostService;
using OtmApi.Data.Dtos;
using OtmApi.Services.ScheduleService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;
using OtmApi.Services.StaffService;
using OtmApi.Services.TournamentService;
using Newtonsoft.Json;
using OtmApi.Data.Entities;
using Microsoft.AspNetCore.RateLimiting;
using OtmApi.Services.OsuApi;
using OtmApi.Services.MapService;
using OtmApi.Services.Players;
using OtmApi.Services.RoundService;

namespace OtmApi.Controllers;

[Route("api/v1/schedule")]
public class ScheduleController(
    IScheduleService scheduleService,
    IMapper mapper,
    IHostService hostService,
    IStaffService staffService,
    ITourneyService tourneyService,
    IOsuApiService osuApiService,
    IMapService mapService,
    IRoundService roundService
    ) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IMapper _mapper = mapper;
    private readonly IHostService _hostService = hostService;
    private readonly IStaffService _staffService = staffService;
    private readonly ITourneyService _tourneyService = tourneyService;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IMapService _mapService = mapService;
    private readonly IRoundService _roundService = roundService;

    [HttpPost("generate-qualifier")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QualsScheduleDto>))]
    public async Task<ActionResult<List<QualsScheduleDto>>> GenerateQualsScheduleAsync([FromBody] GenQualsRequestDto request)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "User not found"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        if (!await _hostService.HostsTournamentAsync(osuId, request.TournamentId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "User is not authorized to generate schedule for this tournament"));

        try
        {
            var res = await _scheduleService.GenerateQualsScheduleAsync(request.TournamentId, request.RoundId, request.StartDate, request.EndDate);
            return Ok(_mapper.Map<List<QualsScheduleDto>>(res));
        }
        catch (AlreadyExistException)
        {

            return Conflict(new ErrorResponse("Conflict", 409, "Quals Schedule already exists for this round"));
        }
    }

    [HttpGet("qualifier/{roundId}")]

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QualsScheduleDto>))]

    public async Task<ActionResult<List<QualsScheduleDto>>> GetQualsScheduleAsync([FromRoute] int roundId)
    {
        try
        {
            var res = await _scheduleService.GetQualsScheduleAsync(roundId);
            return Ok(_mapper.Map<List<QualsScheduleDto>>(res));
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    [HttpPost("tournament/{tournamentId}/qualifier/{sheduleId}/referee")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StaffDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<StaffDto>> SetRefereeAsync(int tournamentId, int sheduleId)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        try
        {
            var staff = await _staffService.GetByIdAsync(osuId, tournamentId);
            if (!await _tourneyService.StaffsInTourneyAsync(tournamentId, osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You do not staff in this tournament"));
            if (!staff.Roles.Contains("referee")) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't have the referee role"));

            var addedRef = await _scheduleService.SetQualsRefereeAsync(sheduleId, osuId);


            return Ok(_mapper.Map<StaffDto>(addedRef));
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }

    }

    [HttpPut("quals-schedule/{scheduleId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QualsScheduleDto))]
    [EnableRateLimiting("fixed")]

    public async Task<ActionResult<QualsSchedulePutDto>> UpdateQualsScheduleAsync(int scheduleId, [FromBody] QualsSchedulePutDto request)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        try
        {


            var staff = await _staffService.GetByIdAsync(osuId, request.TourneyId);
            if (!await _tourneyService.StaffsInTourneyAsync(request.TourneyId, osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You do not staff in this tournament"));
            if (!staff.Roles.Any(r => r == "referee" || r == "admin" || r == "host")) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't have the referee or admin role"));


            // stats stuff
            if (request.MpLinkId != 0 && request.MpLinkId != null && !await _roundService.StatsForMatchExistAsync((int)request.MpLinkId))
            {
                var stats = await MatchToStats(request);
                await _roundService.AddStatsAsync(stats);
            }

            // ######


            await _scheduleService.AddNamesToQualsScheduleAsync(scheduleId, request.Names);
            await _scheduleService.SetQualsMatchIdAsync(scheduleId, request.MpLinkId);
            if (request.RefId == 0) request.RefId = null;
            var qs = await _scheduleService.SetQualsRefereeAsync(scheduleId, request.RefId);

            return Ok(_mapper.Map<QualsScheduleDto>(qs));

        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    [HttpPost("quals-schedule/add-extra")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QualsScheduleDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> QualsScheduleAddExtraAsync([FromBody] QualsScheduleAddExtraDto request)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        try
        {
            var staff = await _staffService.GetByIdAsync(osuId, request.TourneyId);
            if (!await _tourneyService.StaffsInTourneyAsync(request.TourneyId, osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You do not staff in this tournament"));
            if (!staff.Roles.Any(r => r == "referee" || r == "admin" || r == "host")) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't have the appropriate role"));


            var qs = new QualsSchedule
            {
                RoundId = request.RoundId,
                DateTime = request.DateTime,
                Num = request.Num
            };
            var addedQs = await _scheduleService.AddQualsScheduleAsync(qs);
            return Ok(_mapper.Map<QualsScheduleDto>(addedQs));
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    [HttpPut("tournament/{tournamentId}/round/{roundId}/mp-visibility")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult> HandleMpVisibility(int tournamentId, int roundId)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        try
        {
            var staff = await _staffService.GetByIdAsync(osuId, tournamentId);
            if (!await _tourneyService.StaffsInTourneyAsync(tournamentId, osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You do not staff in this tournament"));
            if (!staff.Roles.Any(r => r == "admin" || r == "host")) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't have the host or admin role"));

            await _roundService.ChangeMpVisibilityAsync(roundId);

            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    private async Task<List<Stats>> MatchToStats(QualsSchedulePutDto request)
    {
        var tournament = await _tourneyService.GetByIdAsync(request.TourneyId);
        var games = await _osuApiService.GetMatchGamesV1Async((long)request.MpLinkId!);
        var round = await _roundService.GetRoundByIdAsync(request.RoundId);
        var players = new List<Player>();

        if (tournament!.IsTeamTourney)
        {
            var teams = await _tourneyService.GetAllTeamsAsync(request.TourneyId);
            foreach (var team in teams) players.AddRange(team.Players!);
        }
        else
        {
            players = await _tourneyService.GetAllPlayersAsync(request.TourneyId);
        }



        var statsList = new List<Stats>();
        foreach (var g in games)
        {
            if (round.Mappool!.Any(t => t.Id == g.Beatmap_id))
            {
                foreach (var s in g.Scores)
                {
                    if (players.Any(p => p.Id == s.User_id))
                    {
                        if (statsList.Any(sl => sl.PlayerId == s.User_id && sl.MapId == g.Beatmap_id))
                        {
                            var oldStatIndex = statsList.FindIndex(sl => sl.PlayerId == s.User_id && sl.MapId == g.Beatmap_id);
                            if (statsList[oldStatIndex].Score < s.Score) statsList[oldStatIndex].Score = s.Score;
                        }
                        else
                        {

                            var stat = new Stats
                            {
                                MapId = g.Beatmap_id,
                                Map = round.Mappool!.Single(m => m.Id == g.Beatmap_id),

                                Acc = 0,
                                Mods = [],

                                PlayerId = s.User_id,
                                Player = players.Single(p => p.Id == s.User_id),

                                RoundId = request.RoundId,
                                Round = round,

                                Score = s.Score

                            };
                            statsList.Add(stat);
                        }
                    }
                }
            }
        }
        return statsList;

    }
}