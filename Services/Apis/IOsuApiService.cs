using OtmApi.Data.Entities;
using OtmApi.Utils;

namespace OtmApi.Services.OsuApi;

public interface IOsuApiService
{
    Task<string> GetToken();
    Task<Player[]?> GetPlayers(List<int> ids);
    Task<Player> GetPlayerByUsername(string username);
    Task<List<Game>> GetMatchGamesAsync(long MatchId);
    Task<List<GameV1>> GetMatchGamesV1Async(long matchId);
    Task<List<Beatmap>> GetBeatmapsAsync(List<int> mapIds);
    Task<Attributes> GetBeatmapAttributesAsync(long id, string mod);

}