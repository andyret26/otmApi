using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Dtos;
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

    public async Task DeleteAsync(int id)
    {
        var t = await _db.Tournaments.FindAsync(id);
        if (t == null) throw new NotFoundException("Tournament", id);
        _db.Tournaments.Remove(t);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Tournament>> GetAsync()
    {
        return await _db.Tournaments.OrderByDescending(t => t.Id).ToListAsync();
    }

    public async Task<Tournament?> GetByIdAsync(int id)
    {
        var t = await _db.Tournaments
            .Include(t => t.Teams!).ThenInclude(team => team.Players)
            .Include(t => t.Players!).ThenInclude(p => p.Player)
            .Include(t => t.Rounds)
            .Include(t => t.Staff)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (t == null) throw new NotFoundException("Tournament", id);
        return t;
    }
    public async Task<Tournament> GetByIdSimpleAsync(int id)
    {
        var t = await _db.Tournaments
            .SingleOrDefaultAsync(t => t.Id == id);
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
        return await _db.Tournaments.Where(t => t.HostId == hostId).OrderByDescending(t => t.Id).ToListAsync();

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
        _db.TournamentPlayer.Add(new TournamentPlayer { Player = player, Tournament = tournament });
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
        return await _db.TournamentPlayer.AnyAsync(tp => tp.PlayerId == osuId && tp.TournamentId == tournamentId);
    }

    public async Task<bool> StaffsInTourneyAsync(int tournamentId, int staffId)
    {
        var tournament = await _db.Tournaments.Include(t => t.Staff).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (tournament == null) throw new NotFoundException("Tournament", tournamentId);
        if (tournament.Staff == null) return false;
        return tournament.Staff.Any(s => s.Id == staffId && s.TournamentId == tournamentId);
    }

    public async Task<List<Player>> GetAllPlayersAsync(int tournamentId)
    {
        var t = await _db.Tournaments.Include(t => t.Players)!.ThenInclude(tp => tp.Player).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        return t.Players!.Select(tp => tp.Player).OrderBy(p => p.Username).ToList();
    }

    public async Task<List<TournamentPlayer>> GetAllTournamentPlayersAsync(int tournamentId)
    {
        var t = await _db.Tournaments.Include(t => t.Players)!.ThenInclude(tp => tp.Player).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        return t.Players!.OrderBy(tp => tp.Player.Username).ToList();
    }

    public async Task<List<Team>> GetAllTeamsAsync(int tournamentId)
    {
        var t = await _db.Tournaments.Include(t => t.Teams).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        return t.Teams!.OrderBy(t => t.TeamName).ToList();
    }

    public async Task<List<Staff>> GetAllStaffsAsync(int tournamentId)
    {
        var t = await _db.Tournaments.Include(t => t.Staff).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        return t.Staff!.OrderBy(s => s.Username).ToList();
    }

    public async Task<bool> IsTeamTourneyAsync(int tournamentId)
    {
        var t = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        return t.IsTeamTourney;
    }

    public async Task SetPlayerSeedsAsync(int tournamentId, List<PlayerSeedDto> players)
    {
        var t = await _db.Tournaments.Include(t => t.Players).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        foreach (var player in players)
        {
            var tp = t.Players!.SingleOrDefault(tp => tp.PlayerId == player.Id);
            if (tp == null) throw new NotFoundException("Player", player.Id);
            tp.Seed = player.Seed;
        }
        await _db.SaveChangesAsync();
    }

    public async Task SetTeamSeedsAsync(int tournamentId, List<TeamSeedDto> teams)
    {
        var t = await _db.Tournaments.Include(t => t.Teams).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        foreach (var team in teams)
        {
            var tt = t.Teams!.SingleOrDefault(tt => tt.Id == team.Id);
            if (tt == null) throw new NotFoundException("Team", team.Id);
            tt.Seed = team.Seed;
        }
        await _db.SaveChangesAsync();
    }

    public async Task KnockoutPlayersAsync(int tournamentId, List<int> playerIds)
    {
        var t = await _db.Tournaments.Include(t => t.Players).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        foreach (var player in t.Players!)
        {
            if (playerIds.Contains(player.PlayerId)) player.Isknockout = true;
            else player.Isknockout = false;
        }
        await _db.SaveChangesAsync();
    }

    public async Task KnockoutTeamsAsync(int tournamentId, List<int> teamIds)
    {
        var t = await _db.Tournaments.Include(t => t.Teams).SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        foreach (var team in t.Teams!)
        {
            if (teamIds.Contains(team.Id)) team.Isknockout = true;
            else team.Isknockout = false;
        }
        await _db.SaveChangesAsync();
    }

    public async Task SetHowManyQualifiesAsync(int tournamentId, string howManyQualifies)
    {
        var t = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        t.HowManyQualifies = howManyQualifies;
        await _db.SaveChangesAsync();
    }

    public async Task AddChallongeIdAsync(int tournamentId, int challongeId)
    {
        var t = await _db.Tournaments.SingleOrDefaultAsync(t => t.Id == tournamentId);
        if (t == null) throw new NotFoundException("Tournament", tournamentId);
        t.ChallongeId = challongeId;
        await _db.SaveChangesAsync();
    }
}