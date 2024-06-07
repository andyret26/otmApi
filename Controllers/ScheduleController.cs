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
using OtmApi.Data.Entities;
using Microsoft.AspNetCore.RateLimiting;
using OtmApi.Services.OsuApi;
using OtmApi.Services.MapService;
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
        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TournamentId, ["admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));


            var res = await _scheduleService.GenerateQualsScheduleAsync(request.TournamentId, request.RoundId, request.StartDate, request.EndDate);
            return Ok(_mapper.Map<List<QualsScheduleDto>>(res));
        }
        catch (AlreadyExistException)
        {

            return Conflict(new ErrorResponse("Conflict", 409, "Quals Schedule already exists for this round"));
        }
    }

    [HttpGet("tournament/{tourneyId}/qualifier/{roundId}")]

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<QualsScheduleDto>))]

    public async Task<ActionResult<List<QualsScheduleDto>>> GetQualsScheduleAsync(int tourneyId, [FromRoute] int roundId)
    {

        try
        {
            var round = await _roundService.GetRoundByIdAsync(roundId);
            var res = await _scheduleService.GetQualsScheduleAsync(roundId);
            if (round.IsMpLinksPublic)
            {
                return Ok(_mapper.Map<List<QualsScheduleDto>>(res));
            }

            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, tourneyId, ["admin", "host"]);
            if (!isAuth)
            {
                res.ForEach(r => r.MatchId = null);
            }

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

        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, tournamentId, ["referee", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));


            var osuId = int.Parse(tokenSub!.Value.Split("|")[2]);
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
        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TourneyId, ["referee", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

            // stats stuff
            if (request.MpLinkId != 0 && request.MpLinkId != null && !await _roundService.StatsForMatchExistAsync((int)request.MpLinkId))
            {
                var (playerStats, teamStats) = await MatchToStats(request);
                await _roundService.AddPlayerStatsAsync(playerStats);
                if (teamStats.Count > 0) await _roundService.AddTeamStatsAsync(teamStats);
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

        try
        {

            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TourneyId, ["referee", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

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

        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, tournamentId, ["admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

            await _roundService.ChangeMpVisibilityAsync(roundId);

            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    //TODO

    // [HttpPost("tournament/{tournamentId}/round/{roundId}/generate-schedule")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ScheduleDto>))]

    // public async Task<ActionResult<List<ScheduleDto>>>
    // GenerateScheduleAsync(int tournamentId, int roundId, [FromBody] GenQualsRequestDto request)
    // {
    //     try
    //     {
    //         var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
    //         var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, tournamentId, ["admin", "host"]);
    //         if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

    //         return Ok();
    //     }
    //     catch (AlreadyExistException e)
    //     {
    //         return Conflict(new ErrorResponse("Conflict", 409, e.Message));
    //     }
    // }







    private async Task<(List<PlayerStats>, List<TeamStats>)> MatchToStats(QualsSchedulePutDto request)
    {
        var tournament = await _tourneyService.GetByIdAsync(request.TourneyId);
        var games = await _osuApiService.GetMatchGamesV1Async((long)request.MpLinkId!);
        var round = await _roundService.GetRoundByIdAsync(request.RoundId);
        var players = new List<Player>();
        var teams = new List<Team>();

        if (tournament!.IsTeamTourney)
        {
            teams = await _tourneyService.GetAllTeamsAsync(request.TourneyId);
            foreach (var team in teams) players.AddRange(team.Players!);
        }
        else
        {
            players = await _tourneyService.GetAllPlayersAsync(request.TourneyId);
        }


        var statsList = new List<PlayerStats>();
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

                            var stat = new PlayerStats
                            {
                                MapId = g.Beatmap_id,
                                Map = round.Mappool!.Single(m => m.Id == g.Beatmap_id),

                                Acc = 0,
                                Mods = [],

                                PlayerId = s.User_id,
                                Player = players.Single(p => p.Id == s.User_id),

                                RoundId = request.RoundId,
                                Round = round,

                                Score = s.Score,
                                MatchId = (int)request.MpLinkId!

                            };
                            statsList.Add(stat);
                        }
                    }
                }
            }
        }


        var teamStatsList = new List<TeamStats>();
        if (tournament.IsTeamTourney)
        {
            // creat teamStats in a seperate list
            foreach (var stat in statsList)
            {
                if (teams.Any(t => t.Players!.Any(p => p.Id == stat.PlayerId)))
                {
                    var team = teams.Single(t => t.Players!.Any(p => p.Id == stat.PlayerId));
                    if (teamStatsList.Any(ts => ts.TeamId == team.Id && ts.MapId == stat.MapId))
                    {
                        var oldStatIndex = teamStatsList.FindIndex(ts => ts.TeamId == team.Id && ts.MapId == stat.MapId);
                        teamStatsList[oldStatIndex].TotalScore += stat.Score;
                        teamStatsList[oldStatIndex].AvgScore = teamStatsList[oldStatIndex].TotalScore / int.Parse(tournament.Format[..1]);
                    }
                    else
                    {
                        var teamStat = new TeamStats
                        {
                            MapId = stat.MapId,
                            Map = stat.Map,

                            TeamId = team.Id,
                            Team = team,

                            RoundId = request.RoundId,
                            Round = round,

                            TotalScore = stat.Score,
                            AvgScore = stat.Score,
                            Acc = 0,

                            MatchId = (int)request.MpLinkId!
                        };
                        teamStatsList.Add(teamStat);
                    }
                }

            }
            return (statsList, teamStatsList);
        }


        return (statsList, []);

    }
}