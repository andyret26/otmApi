using Microsoft.EntityFrameworkCore;
using OtmApi.Data;
using OtmApi.Data.Entities;
using OtmApi.Utils.Exceptions;

namespace OtmApi.Services.MapService;

public class MapService(DataContext db) : IMapService
{
    private readonly DataContext _db = db;

    public async Task<TMapSuggestion> AddMapSuggestion(TMapSuggestion mapSuggestion)
    {
        var addedSuggestion = await _db.MapSuggestions.AddAsync(mapSuggestion);
        await _db.SaveChangesAsync();

        return addedSuggestion.Entity;
    }


    public async Task<TMapSuggestion> GetMapSuggestionAsync(int mapId, string mod)
    {
        var ms = await _db.MapSuggestions.FirstOrDefaultAsync(s => s.Id == mapId && s.Mod.Substring(0, 2) == mod.Substring(0, 2));
        if (ms == null) throw new NotFoundException("MapSuggestion", mapId);
        return ms;

    }

    public async Task<bool> MapSuggestionExists(int mapId, string mod)
    {
        return await _db.MapSuggestions.AnyAsync(s => s.Id == mapId && s.Mod.Substring(0, 2) == mod.Substring(0, 2));
    }

    public async Task<List<TMap>> GetMapsByRoundIdAsync(int roundId)
    {
        var maps = await _db.Maps.Where(m => m.Rounds!.Any(r => r.Id == roundId)).ToListAsync();
        return maps;
    }



}