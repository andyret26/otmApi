
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtmApi.Services.HostService;
using OtmApi.Data.Dtos;
using OtmApi.Services.OsuApi;
using OtmApi.Services.Players;
using OtmApi.Services.TournamentService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Controllers;

[Route("api/v1/host")]
[ApiController()]
[Tags("OTM Host")]
[Consumes("application/Json")]
[Produces("application/Json")]
public class HostController(
    IPlayerService playerService,
    IOsuApiService osuApiService,
    IHostService hostService,
    ITourneyService tournamentService,
    IMapper mapper
        ) : ControllerBase
{
    private readonly IPlayerService _playerService = playerService;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IHostService _hostService = hostService;
    private readonly ITourneyService _tournamentService = tournamentService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("test")]
    public ActionResult Test()
    {
        return Ok("Host Controller is working");
    }

    [HttpPost("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize]
    public async Task<ActionResult> PostHost(int id)
    {
        var tokenSub = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenSub == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Invalid token"));
        if (tokenSub.Value.Split("|")[2] != id.ToString()) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Token does not match id"));

        if (await _hostService.ExistsAsync(id)) return Conflict(new ErrorResponse("Conflict", 409, "Host already exists"));

        try
        {

            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
            {
                var players = await _osuApiService.GetPlayers(new List<int> { id });
                if (players!.Length == 0) return NotFound();
                player = await _playerService.PostAsync(players[0]);
            }
            var newHost = new Data.Entities.Host
            {
                Id = player.Id,
                Username = player.Username,
            };

            var addedHost = await _hostService.AddAsync(newHost);
            return CreatedAtAction(null, addedHost); // cahnge to created at when get by id is made
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet("{id}/tournaments")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TournamentDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<TournamentDto>>> GetAllByHostId(int id)
    {
        try
        {
            return _mapper.Map<List<TournamentDto>>(await _tournamentService.GetAllByHostIdAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
        catch (Exception)
        {

            throw;
        }
    }

}
