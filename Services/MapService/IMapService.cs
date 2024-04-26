using OtmApi.Data.Entities;

namespace OtmApi.Services.MapService;

public interface IMapService
{
    public Task<TMapSuggestion> AddMapSuggestion(TMapSuggestion mapSuggestion);
    public Task<bool> MapSuggestionExists(int mapId, string mod);

    public Task<TMapSuggestion> GetMapSuggestionAsync(int mapId, string mod);
}