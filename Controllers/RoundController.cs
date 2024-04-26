using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Services.MapService;
using OtmApi.Services.OsuApi;
using OtmApi.Services.RoundService;
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
    IMapService mapService
    ) : ControllerBase
{
    private readonly IRoundService _roundService = roundService;
    private readonly IMapper _mapper = mapper;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IMapService _mapService = mapService;

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
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MapSuggestionDto>> AddSuggestionToRound(int roundId, [FromBody] PostSuggestionDto request)
    {
        try
        {

            var map = await _osuApiService.GetBeatmapsAsync([request.MapId]);
            if (map.IsNullOrEmpty()) return NotFound(new ErrorResponse("NotFound", 404, "Map not found"));

            // TODO if dt or hr get map attributes from https://osu.ppy.sh/docs/index.html#get-beatmap-attributes

            var mapSuggestion = _mapper.Map<TMapSuggestion>(map[0]);
            mapSuggestion.Mod = request.Mod;
            mapSuggestion.Notes = request.Notes;

            var addedSuggestion = await _mapService.AddMapSuggestion(mapSuggestion);
            var updatedRound = await _roundService.AddSuggestionToRound(roundId, addedSuggestion);


            var returnDto = _mapper.Map<MapSuggestionDto>(addedSuggestion);
            return Ok(returnDto);
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("NotFound", 404, e.Message));
        }

    }
}