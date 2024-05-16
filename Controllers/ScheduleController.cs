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

namespace OtmApi.Controllers;

[Route("api/v1/schedule")]
public class ScheduleController(
    IScheduleService scheduleService,
    IMapper mapper,
    IHostService hostService,
    IStaffService staffService,
    ITourneyService tourneyService
    ) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IMapper _mapper = mapper;
    private readonly IHostService _hostService = hostService;
    private readonly IStaffService _staffService = staffService;
    private readonly ITourneyService _tourneyService = tourneyService;

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

    public async Task<ActionResult<QualsSchedulePutDto>> UpdateQualsScheduleAsync(int scheduleId, [FromBody] QualsSchedulePutDto request)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
        var osuId = int.Parse(tokenSub.Value.Split("|")[2]);
        try
        {

            var staff = await _staffService.GetByIdAsync(osuId, request.TourneyId);
            if (!await _tourneyService.StaffsInTourneyAsync(request.TourneyId, osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You do not staff in this tournament"));
            if (!staff.Roles.Any(r => r == "referee" || r == "admin")) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't have the referee or admin role"));

            await _scheduleService.AddNamesToQualsScheduleAsync(scheduleId, request.Names);
            if (request.RefId == 0) request.RefId = null;
            var qs = await _scheduleService.SetQualsRefereeAsync(scheduleId, request.RefId);

            return Ok(_mapper.Map<QualsScheduleDto>(qs));

        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }


}