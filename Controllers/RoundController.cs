using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Services.MapService;
using OtmApi.Services.OsuApi;
using OtmApi.Services.RoundService;
using OtmApi.Services.StaffService;
using OtmApi.Services.TournamentService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Controllers;

[Route("api/v1/round")]
[ApiController]
[Produces("application/Json")]
[Consumes("application/Json")]


public class RoundController(
    IRoundService roundService,
    IMapper mapper,
    IOsuApiService osuApiService,
    IMapService mapService,
    ITourneyService tourneyService,
    IStaffService staffService
    ) : ControllerBase
{
    private readonly IRoundService _roundService = roundService;
    private readonly IMapper _mapper = mapper;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IMapService _mapService = mapService;
    private readonly ITourneyService _tourneyService = tourneyService;
    private readonly IStaffService _staffService = staffService;

    [HttpGet("{id}")]
    public async Task<ActionResult<RoundDetaildDto>> GetRoundById(int id)
    {
        try
        {
            var round = await _roundService.GetRoundByIdAsync(id);
            return Ok(_mapper.Map<RoundDetaildDto>(round));

        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }

    }

    [HttpPost("{roundId}/suggestion")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MapSuggestionDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EnableRateLimiting("fixed")]
    public async Task<ActionResult<MapSuggestionDto>> AddSuggestionToRound(int roundId, [FromBody] PostSuggestionDto request)
    {
        if (roundId != request.RoundId) return BadRequest(new ErrorResponse("BadRequest", 400, "RoundId in the path does not match the RoundId in the body"));
        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TournamentId, ["mappooler", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

            TMapSuggestion mapSuggestion;
            if (await _mapService.MapSuggestionExists(request.MapId, request.Mod))
            {
                mapSuggestion = await _mapService.GetMapSuggestionAsync(request.MapId, request.Mod);
            }

            else
            {
                if (await _mapService.MapSuggestionExists(request.MapId, "NM"))
                {
                    mapSuggestion = await _mapService.GetMapSuggestionAsync(request.MapId, "NM");

                }
                else
                {
                    var map = await _osuApiService.GetBeatmapsAsync([request.MapId]);
                    if (map.IsNullOrEmpty()) return NotFound(new ErrorResponse("NotFound", 404, "Map not found"));

                    mapSuggestion = _mapper.Map<TMapSuggestion>(map[0]);

                }


                if (mapSuggestion.Mod == null && (request.Mod[..2] == "DT" || request.Mod[..2] == "HR"))
                {
                    var attributes = await _osuApiService.GetBeatmapAttributesAsync(mapSuggestion.Id, request.Mod[..2]);
                    AttributeCalculate(mapSuggestion, attributes, request.Mod);

                }
            }

            if (mapSuggestion.Id == request.MapId && mapSuggestion.Mod == request.Mod)
            {
                await _roundService.AddSuggestionToRound(roundId, mapSuggestion);
                return Ok(_mapper.Map<MapSuggestionDto>(mapSuggestion));
            }

            mapSuggestion.Mod = request.Mod;
            mapSuggestion.Notes = request.Notes;

            var suggestionToAddToRound = await _mapService.AddMapSuggestion(mapSuggestion);

            await _roundService.AddSuggestionToRound(roundId, suggestionToAddToRound);


            var returnDto = _mapper.Map<MapSuggestionDto>(suggestionToAddToRound);
            return Ok(returnDto);
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }

    }

    [HttpPost("{roundId}/mappool")]
    [Authorize]
    public async Task<ActionResult<MapDto>> AddSuggestionToPool(string roundId, [FromBody] PostSuggestionDto request)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TournamentId, ["mappooler", "admin", "host"]);
        if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));
        try
        {


            var map = await _roundService.AddSuggestionToPoolAsync(int.Parse(roundId), request.MapId, request.Mod);
            return Ok(_mapper.Map<MapDto>(map));
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpPost("{roundId}/mappool/remove-suggestion")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveSuggestionFromPool(int roundId, [FromBody] PostSuggestionDto request)
    {
        if (roundId != request.RoundId) return BadRequest(new ErrorResponse("BadRequest", 400, "RoundId in the path does not match the RoundId in the body"));

        try
        {
            var userTokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(userTokenSub, _staffService, _tourneyService, request.TournamentId, ["mappooler", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));


            await _roundService.RemoveSuggestionFromPoolAsync(roundId, request.MapId, request.Mod);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }
    }

    [HttpGet("{roundId}/stats")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MapWithStatsDto))]
    public async Task<ActionResult<List<MapWithStatsDto>>> GetStats(int roundId)
    {
        var maps = await _roundService.GetMapsAsync(roundId);
        return Ok(_mapper.Map<List<MapWithStatsDto>>(maps));

    }

    [HttpDelete("{roundId}/delete-suggestion")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteSuggestion(int roundId, [FromBody] PostSuggestionDto request)
    {
        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, request.TournamentId, ["mappooler", "admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

            await _roundService.DeleteSuggestionFromRoundAsync(roundId, request.MapId, request.Mod);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }
    }

    [HttpPut("{roundId}/tournament/{tournamentId}/stats-visibility")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<ActionResult> ChangeStatsVisibility(int roundId, int tournamentId)
    {
        try
        {
            var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
            var (isAuth, msg) = await Auth.IsAuthorized(tokenSub, _staffService, _tourneyService, tournamentId, ["admin", "host"]);
            if (!isAuth) return Unauthorized(new ErrorResponse("Unauthorized", 401, msg));

            await _roundService.ChangeStatsVisibilityAsync(roundId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }

    }


    private static void AttributeCalculate(TMapSuggestion mapSuggestion, Attributes attributes, string mod)
    {
        mapSuggestion.Difficulty_rating = attributes.Star_rating;
        mapSuggestion.Ar = attributes.Approach_rate;
        mapSuggestion.Accuracy = attributes.Overall_difficulty;
        mapSuggestion.Bpm = mod[..2] == "DT" ? (decimal)1.5 * mapSuggestion.Bpm : mapSuggestion.Bpm;
        mapSuggestion.Total_length = mod[..2] == "DT" ? mapSuggestion.Total_length / (decimal)1.5 : mapSuggestion.Total_length;
        mapSuggestion.Cs = mod[..2] == "HR" ? (decimal)1.3 * mapSuggestion.Cs : mapSuggestion.Cs;
        if (mapSuggestion.Cs > 10) mapSuggestion.Cs = 10;
    }
}
