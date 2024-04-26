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

        return addedSuggestion.Entity;
    }

    public async Task<TMapSuggestion> GetMapSuggestionAsync(int mapId, string mod)
    {
        var ms = await _db.MapSuggestions.SingleOrDefaultAsync(s => s.Id == mapId && s.Mod == mod);
        if (ms == null) throw new NotFoundException("MapSuggestion", mapId);
        return ms;

    }

    public async Task<bool> MapSuggestionExists(int mapId, string mod)
    {
        return await _db.MapSuggestions.AnyAsync(s => s.Id == mapId && s.Mod == mod);
    }




}