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


}