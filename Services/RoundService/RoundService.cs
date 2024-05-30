using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.RoundService;

public class RoundService(DataContext db, IMapper mapper) : IRoundService
{
    private readonly DataContext _db = db;
    private readonly IMapper _mapper = mapper;

    public async Task<Round> AddSuggestionToRound(int roundId, TMapSuggestion mapSuggestion)
    {
        var round = _db.Rounds.Include(r => r.MapSuggestions).SingleOrDefault(r => r.Id == roundId);
        if (round == null) throw new NotFoundException("Round", roundId);
        if (round.MapSuggestions == null) round.MapSuggestions = new List<TMapSuggestion>();

        var ms = await _db.MapSuggestions.SingleOrDefaultAsync(m => m.Id == mapSuggestion.Id);
        if (ms == null) throw new NotFoundException("MapSuggestion", mapSuggestion.Id);


        _db.MapSuggestions.Attach(ms);

        round.MapSuggestions.Add(ms);
        await _db.SaveChangesAsync();
        return round;
    }

    public async Task<Round> GetRoundByIdAsync(int id)
    {
        var r = await _db.Rounds
            .Include(r => r.MapSuggestions)
            .Include(r => r.Mappool!)
                .ThenInclude(m => m.PlayerStats!)
                    .ThenInclude(p => p.Player)
                        .ThenInclude(p => p.Tournaments)
            .Include(r => r.Mappool!)
                .ThenInclude(m => m.TeamStats!)
                    .ThenInclude(t => t.Team)
            .Include(r => r.Tournament)
            .SingleOrDefaultAsync(r => r.Id == id);
        if (r == null) throw new NotFoundException("Round", id);
        return r;
    }
    public async Task<TMap> AddSuggestionToPoolAsync(int roundId, int mapId, string mod)
    {
        var mapSuggestion = await _db.MapSuggestions.SingleOrDefaultAsync(ms => ms.Id == mapId && ms.Mod == mod);
        if (mapSuggestion == null) throw new NotFoundException("MapSuggestion", mapId);

        var round = await _db.Rounds.Include(r => r.Mappool).Include(r => r.MapSuggestions).SingleOrDefaultAsync(r => r.Id == roundId);
        if (round == null) throw new NotFoundException("Round", roundId);

        if (round.Mappool == null) round.Mappool = new List<TMap>();

        var map = await _db.Maps.SingleOrDefaultAsync(m => m.Id == mapId && m.Mod == mod);
        if (map == null)
        {
            var newMap = _mapper.Map<TMap>(mapSuggestion);
            round.Mappool.Add(newMap);
            await _db.SaveChangesAsync();
            return newMap;
        }

        round.Mappool.Add(map);
        await _db.SaveChangesAsync();
        return map;
    }

    public async Task RemoveSuggestionFromPoolAsync(int roundId, int mapId, string mod)
    {
        var r = await _db.Rounds.Include(r => r.Mappool).SingleOrDefaultAsync(r => r.Id == roundId);
        if (r == null) throw new NotFoundException("Round", roundId);
        var mIndex = r.Mappool!.FindIndex(m => m.Id == mapId && m.Mod == mod);
        if (mIndex == -1) throw new NotFoundException("Map", mapId);
        r.Mappool!.RemoveAt(mIndex);
        await _db.SaveChangesAsync();
        return;
    }

    public async Task<List<PlayerStats>> AddPlayerStatsAsync(List<PlayerStats> stats)
    {
        await _db.PlayerStats.AddRangeAsync(stats);
        await _db.SaveChangesAsync();
        return stats;
    }
    public async Task<List<TeamStats>> AddTeamStatsAsync(List<TeamStats> stats)
    {
        await _db.TeamStats.AddRangeAsync(stats);
        await _db.SaveChangesAsync();
        return stats;
    }

    public Task<bool> StatsForMatchExistAsync(int matchId)
    {
        return _db.PlayerStats.AnyAsync(s => s.MatchId == matchId);
    }

    public async Task<bool> ChangeMpVisibilityAsync(int roundId)
    {
        var round = await _db.Rounds.SingleOrDefaultAsync(r => r.Id == roundId);
        if (round == null) throw new NotFoundException("Round", roundId);
        round.IsMpLinksPublic = !round.IsMpLinksPublic;
        await _db.SaveChangesAsync();
        return round.IsMpLinksPublic;
    }

    public async Task<(List<PlayerStats>, List<TeamStats>)> GetStats(int roundId)
    {
        var pStats = await _db.PlayerStats.Where(s => s.RoundId == roundId).ToListAsync();
        var tStats = await _db.TeamStats.Where(s => s.RoundId == roundId).ToListAsync();

        return (pStats, tStats);
    }

    public async Task<List<TMap>> GetMapsAsync(int roundId)
    {
        var r = await _db.Rounds
            .Include(r => r.Mappool!)
                .ThenInclude(m => m.PlayerStats!)
                    .ThenInclude(p => p.Player)
                        .ThenInclude(p => p.Tournaments)
            .Include(r => r.Mappool!)
                .ThenInclude(m => m.TeamStats!)
                    .ThenInclude(t => t.Team)
            .SingleOrDefaultAsync(r => r.Id == roundId);
        if (r == null) throw new NotFoundException("Round", roundId);
        return r.Mappool!;
    }

    public async Task DeleteSuggestionFromRoundAsync(int roundId, int mapId, string mod)
    {
        var r = await _db.Rounds.Include(r => r.MapSuggestions).SingleOrDefaultAsync(r => r.Id == roundId);
        if (r == null) throw new NotFoundException("Round", roundId);
        var mIndex = r.MapSuggestions!.FindIndex(m => m.Id == mapId && m.Mod == mod);
        if (mIndex == -1) throw new NotFoundException("MapSuggestion", mapId);
        r.MapSuggestions!.RemoveAt(mIndex);
        await _db.SaveChangesAsync();
        return;
    }
}