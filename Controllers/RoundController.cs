using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Services.MapService;
using OtmApi.Services.OsuApi;
using OtmApi.Services.RoundService;
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
    ITourneyService tourneyService
    ) : ControllerBase
{
    private readonly IRoundService _roundService = roundService;
    private readonly IMapper _mapper = mapper;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IMapService _mapService = mapService;
    private readonly ITourneyService _tourneyService = tourneyService;

    [HttpGet("{id}")]
    public async Task<ActionResult<RoundWithMapsDto>> GetRoundById(int id)
    {
        try
        {
            var round = await _roundService.GetRoundByIdAsync(id);
            return Ok(_mapper.Map<RoundWithMapsDto>(round));

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


            // ### auth stuff ###
            var sub = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (sub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));
            var osuId = int.Parse(sub.Split("|")[2]);

            var tourney = await _tourneyService.GetByIdAsync(request.TournamentId);
            if (tourney!.HostId != osuId)
            {
                if (tourney!.Staff == null || !tourney.Staff.Any(s => s.Id == osuId)) return Unauthorized(new ErrorResponse("Unauthorized", 401, "You don't Staff in this tournament"));
            }

            // ### auth stuff end ###

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


                if (mapSuggestion.Mod != request.Mod && (request.Mod == "DT" || request.Mod == "HR"))
                {
                    var attributes = await _osuApiService.GetBeatmapAttributesAsync(mapSuggestion.Id, request.Mod);
                    AttributeCalculate(mapSuggestion, attributes, request.Mod);

                }
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

    [HttpPost("{roundId}/mappool/")]
    public async Task<ActionResult<MapDto>> AddSuggestionToPool(string roundId, [FromBody] PostSuggestionDto request)
    {
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


    private static void AttributeCalculate(TMapSuggestion mapSuggestion, Attributes attributes, string mod)
    {
        mapSuggestion.Difficulty_rating = attributes.Star_rating;
        mapSuggestion.Ar = attributes.Approach_rate;
        mapSuggestion.Accuracy = attributes.Overall_difficulty;
        mapSuggestion.Bpm = mod == "DT" ? (decimal)1.5 * mapSuggestion.Bpm : mapSuggestion.Bpm;
        mapSuggestion.Total_length = mod == "DT" ? mapSuggestion.Total_length / (decimal)1.5 : mapSuggestion.Total_length;
        mapSuggestion.Cs = mod == "HR" ? (decimal)1.3 * mapSuggestion.Cs : mapSuggestion.Cs;
        if (mapSuggestion.Cs > 10) mapSuggestion.Cs = 10;
    }
}
