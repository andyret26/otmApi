using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmtApi.Services.HostService;
using OtmApi.Data.Dtos;
using OtmApi.Services.ScheduleService;
using OtmApi.Services.TournamentService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Controllers;

[Route("api/v1/schedule")]
public class ScheduleController(
    IScheduleService scheduleService,
    IMapper mapper,
    IHostService hostService
    ) : ControllerBase
{
    private readonly IScheduleService _scheduleService = scheduleService;
    private readonly IMapper _mapper = mapper;
    private readonly IHostService _hostService = hostService;

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
}