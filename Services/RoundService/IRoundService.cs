using OtmApi.Data.Entities;

namespace OtmApi.Services.RoundService;

public interface IRoundService
{
    public Task<Round> GetRoundByIdAsync(int id);
    public Task<Round> AddSuggestionToRound(int roundId, TMapSuggestion mapSuggestion);
    public Task<TMap> AddSuggestionToPoolAsync(int roundId, int mapId, string mod);
    public Task<List<Stats>> AddStatsAsync(List<Stats> stats);
    public Task<bool> StatsForMatchExistAsync(int matchId);
    public Task<bool> ChangeMpVisibilityAsync(int roundId);
}