using OtmApi.Data.Entities;

namespace OtmApi.Services.MapService;

public interface IMapService
{
    public Task<TMapSuggestion> AddMapSuggestion(TMapSuggestion mapSuggestion);
}