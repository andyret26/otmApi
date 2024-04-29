using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.TournamentService;

public class TourneyService(DataContext db) : ITourneyService
{
    private readonly DataContext _db = db;

    public async Task<Tournament> AddAsync(Tournament tournament)
    {
        var tAdded = await _db.Tournaments.AddAsync(tournament);
        await _db.SaveChangesAsync();
        return tAdded.Entity;
    }

    public Task<Tournament?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Tournament>> GetAsync()
    {
        return await _db.Tournaments.OrderByDescending(t => t.Id).ToListAsync();
    }

    public async Task<Tournament?> GetByIdAsync(int id)
    {
        var t = await _db.Tournaments
            .Include(t => t.Teams!).ThenInclude(team => team.Players)
            .Include(t => t.Players)
            .Include(t => t.Rounds)
            .Include(t => t.Staff)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (t == null) throw new NotFoundException("Tournament", id);
        return t;
    }

    public Task<Tournament?> UpdateAsync(Tournament tournament)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Tournament>> GetAllByHostIdAsync(int hostId)
    {
        if (!await _db.Hosts.AnyAsync((h) => h.Id == hostId)) throw new NotFoundException("Host", hostId);
        return await _db.Tournaments.Where(t => t.HostId == hostId).ToListAsync();

    }

    public async Task<Tournament> AddTeamAsync(int tournamentId, Team team)
    {
        var t = _db.Tournaments.SingleOrDefault(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        if (t.Teams == null) t.Teams = new List<Team>();
        t.Teams.Add(team);
        await _db.SaveChangesAsync();
        return t;

    }

    public async Task<bool> TeamNameExistsInTournamentAsync(int tournamentId, string teamName)
    {
        var t = await GetByIdAsync(tournamentId);
        return t!.Teams!.Any(t => t.TeamName == teamName);
    }
    public async Task<List<int>> PlayerExistsInTeamTournamentAsync(int tournamentId, List<int> playerIds)
    {
        // return players that alreay exists
        var t = await GetByIdAsync(tournamentId);
        List<int> players = new();
        foreach (var team in t!.Teams!)
        {
            foreach (var player in team!.Players!)
            {
                if (playerIds.Contains(player.Id))
                {
                    if (!players.Contains(player.Id))
                    {
                        players.Add(player.Id);
                    }
                }
            }
        }
        return players;
    }

    public async Task<Player> AddPlayerAsync(int tournamentId, Player player)
    {
        var tournament = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament", tournamentId);
        if (tournament.Players == null) tournament.Players = new List<Player>();
        tournament.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task<Round> AddRoundAsync(int tournamentId, Round round)
    {
        var tourney = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (tourney == null) throw new NotFoundException("Tournament", tournamentId);
        if (tourney.Rounds == null) tourney.Rounds = new List<Round>();
        tourney.Rounds.Add(round);
        await _db.SaveChangesAsync();
        return round;
    }

    public async Task<bool> PlayerExistsInTourneyAsync(int tournamentId, int osuId)
    {
        var tournament = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament", tournamentId);
        if (tournament.Players == null) return false;
        return tournament.Players!.Any(p => p.Id == osuId);
    }

    public async Task<bool> StaffsInTourneyAsync(int tournamentId, int staffId)
    {
        var tournament = await _db.Tournaments.Include(t => t.Staff).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament", tournamentId);
        if (tournament.Staff == null) return false;
        return tournament.Staff.Any(s => s.Id == staffId);
    }
}