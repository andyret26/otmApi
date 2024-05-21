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
        var round = _db.Rounds.SingleOrDefault(r => r.Id == roundId);
        if (round == null) throw new NotFoundException("Round", roundId);
        if (round.MapSuggestions == null) round.MapSuggestions = new List<TMapSuggestion>();

        round.MapSuggestions.Add(mapSuggestion);
        await _db.SaveChangesAsync();
        return round;
    }

    public async Task<Round> GetRoundByIdAsync(int id)
    {
        var round = await _db.Rounds.Include(r => r.Mappool).Include(r => r.MapSuggestions).SingleOrDefaultAsync(r => r.Id == id);
        if (round == null) throw new NotFoundException("Round", id);
        return round;
    }
    public async Task<TMap> AddSuggestionToPoolAsync(int roundId, int mapId, string mod)
    {
        var mapSuggestion = await _db.MapSuggestions.SingleOrDefaultAsync(ms => ms.Id == mapId && ms.Mod == mod);
        if (mapSuggestion == null) throw new NotFoundException("MapSuggestion", mapId);

        var round = await _db.Rounds.Include(r => r.Mappool).Include(r => r.MapSuggestions).SingleOrDefaultAsync(r => r.Id == roundId);
        if (round == null) throw new NotFoundException("Round", roundId);

        if (round.Mappool == null) round.Mappool = new List<TMap>();


        var map = _mapper.Map<TMap>(mapSuggestion);
        round.Mappool.Add(map);
        await _db.SaveChangesAsync();
        return map;
    }

    public async Task<List<Stats>> AddStatsAsync(List<Stats> stats)
    {
        await _db.Stats.AddRangeAsync(stats);
        await _db.SaveChangesAsync();
        return stats;
    }

    public Task<bool> StatsForMatchExistAsync(int matchId)
    {
        return _db.Stats.AnyAsync(s => s.MatchId == matchId);
    }
}