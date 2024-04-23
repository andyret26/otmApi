using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using OtmApi.Data.Dtos;
using OtmApi.Data.Entities;
using OtmApi.Services.OsuApi;
using OtmApi.Services.Players;
using OtmApi.Services.TournamentService;
using OtmApi.Utils;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Controllers;

[Route("api/v1/tournament")]
[ApiController]
[Produces("application/Json")]
[Consumes("application/Json")]
public class TournamentController(
    IMapper mapper,
    ITourneyService tourneyService,
    IOsuApiService osuApiService,
    IPlayerService playerService
        ) : ControllerBase
{
    private readonly IMapper _mapper = mapper;
    private readonly ITourneyService _tourneyService = tourneyService;
    private readonly IOsuApiService _osuApiService = osuApiService;
    private readonly IPlayerService _playerService = playerService;


    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TournamentDto))]
    public async Task<ActionResult<TournamentDto>> PostTournament(PostTourneyDto dto)
    {
        var authSub = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (authSub == null) return Unauthorized();

        try
        {
            var tToAdd = _mapper.Map<Tournament>(dto);
            tToAdd.HostId = int.Parse(authSub.Split("|")[2]);

            var addedTourney = await _tourneyService.AddAsync(tToAdd);
            var dtoToReturn = _mapper.Map<TournamentDto>(addedTourney);

            return CreatedAtAction("GetTournament", new { id = addedTourney.Id }, dtoToReturn);
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
        {
            Npgsql.PostgresException ex = (Npgsql.PostgresException)e.InnerException!;
            if (ex.SqlState == "23505") return Conflict(new ErrorResponse("Conflict", 409, "Tournament with that name already exists"));
            if (ex.SqlState == "23514") return UnprocessableEntity(new ErrorResponse("Unprocessable Entity", 422, "Some fields are missing or invalid"));
            return BadRequest("Something went wrong");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TournamentDto>> GetTournament(int id)
    {
        try
        {
            var tourney = await _tourneyService.GetByIdAsync(id);
            var dtoToReturn = _mapper.Map<TournamentDto>(tourney);
            return Ok(dtoToReturn);

        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
    }

    [HttpPost("{tournamentId}/register-team")]
    [EnableRateLimiting("fixed")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<ActionResult> RegisterTeam(int tournamentId, RegistrationDto regsDto)
    {
        if (regsDto.TeamName.IsNullOrEmpty()) return BadRequest(new ErrorResponse("Bad Request", 400, "Team name is required"));
        if (await _tourneyService.TeamNameExistsInTournamentAsync(tournamentId, regsDto.TeamName)) return Conflict(new ErrorResponse("Conflict", 409, "Team with that name already exists in this tournament"));

        var playersThatHasTeam = await _tourneyService.PlayerExistsInTeamTournamentAsync(tournamentId, regsDto.Players.Select(p => p.OsuUserId).ToList());
        if (playersThatHasTeam.Count > 0) return Conflict(new ErrorResponse("Conflict", 409, $"These players alread is in a team: {string.Join(',', playersThatHasTeam)}"));


        if (regsDto.Players.Count <= 1) return BadRequest(new ErrorResponse("Bad Request", 400, "You need at least 2 players to register a team"));

        // Remove players with no osu id
        regsDto.Players.Where(p => p.OsuUserId == 0).ToList().ForEach(p => regsDto.Players.Remove(p));


        List<int> playerIds = new();
        regsDto.Players.ForEach(p => playerIds.Add(p.OsuUserId));
        try
        {
            var playersToAddToDb = await _osuApiService.GetPlayers(playerIds);
            if (playersToAddToDb != null) await _playerService.AddMultipleAsync(playersToAddToDb!.ToList());


            foreach (var p in regsDto.Players)
            {
                await _playerService.UpdateDiscordUsername(p.OsuUserId, p.DiscordUsername);
            }

            Team team = new()
            {
                TeamName = regsDto.TeamName,
                Players = await _playerService.GetMultipleById(playerIds)
            };

            var TournamentTeamGotAddedTo = await _tourneyService.AddTeamAsync(tournamentId, team);
            return Ok(_mapper.Map<TournamentDto>(TournamentTeamGotAddedTo));


        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }
        catch (System.Exception)
        {

            throw;
        }

    }

    [HttpPost("{tournamentId}/register-player")]
    [EnableRateLimiting("fixed")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlayerDto>> RegisterPlayer(int tournamentId, PlayerRegistration regsDto)
    {

        if (regsDto.OsuUserId == 0) return BadRequest(new ErrorResponse("Bad Request", 400, "Osu user id is required"));

        try
        {
            var playersToAdd = await _osuApiService.GetPlayers(new List<int> { regsDto.OsuUserId });
            if (playersToAdd != null) await _playerService.PostAsync(playersToAdd[0]);

            await _playerService.UpdateDiscordUsername(regsDto.OsuUserId, regsDto.DiscordUsername);


            var pToAdd = await _playerService.GetByIdAsync(regsDto.OsuUserId);
            if (pToAdd == null) return NotFound(new ErrorResponse("Not Found", 404, "Player not found"));
            var playerAdded = await _tourneyService.AddPlayerAsync(tournamentId, pToAdd);

            return Ok(_mapper.Map<PlayerDto>(playerAdded));

        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
        {
            Npgsql.PostgresException ex = (Npgsql.PostgresException)e.InnerException!;
            if (ex.SqlState == "23505") return Conflict(new ErrorResponse("Conflict", 409, "Player already exists in this tournament"));
            return BadRequest("Something went wrong");
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse("Not Found", 404, e.Message));
        }

    }

    [HttpPost("{tournamentId}/add-round")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoundDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RoundDto>> AddRound(int tournamentId, [FromBody] RoundPostDto roundPostDto)
    {
        var tokenId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (tokenId == null) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Unauthorized"));

        var tournament = await _tourneyService.GetByIdAsync(tournamentId);
        if (tournament == null) return NotFound(new ErrorResponse("Not Found", 404, $"Tournament with id {tournamentId} not found"));

        if (tokenId.Value.Split("|")[2] != tournament.HostId.ToString()) return Unauthorized(new ErrorResponse("Unauthorized", 401, "Cannot add round to a tournament you do not own"));

        var round = new Round
        {
            Name = roundPostDto.Name
        };

        var addedRound = await _tourneyService.AddRoundAsync(tournamentId, round);
        return Ok(_mapper.Map<RoundDto>(addedRound));
    }

}