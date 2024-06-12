
using System.Text;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using OtmApi.Services.TournamentService;
using OtmApi.Utils;

namespace OtmApi.Services.Apis;

public class ChallongeApiService(ITourneyService tourneyService) : IChallongeApiService
{
    private readonly string? API_KEY = Environment.GetEnvironmentVariable("CHALLONGE_API_KEY");
    private readonly string BASE_URL = "https://api.challonge.com/v1";
    private readonly ITourneyService _tourneyService = tourneyService;

    public async Task CreateTournamentAsync(int tournamentId, string tournamentName)
    {
        if (API_KEY == null) throw new Exception("CHALLONGE_API_KEY environment variable not set");

        var http = new HttpClient();

        var parameters = $"?api_key={API_KEY}&tournament[name]={tournamentName}&tournament[tournament_type]=double%20elimination";

        var fullUrl = BASE_URL + "/tournaments.json" + parameters;


        var resp = await http.PostAsync(fullUrl, null);

        if (resp.IsSuccessStatusCode)
        {
            var content = await resp.Content.ReadAsStringAsync();
            var respData = JsonConvert.DeserializeObject<ChallongeTournamentResp>(content);
            await _tourneyService.AddChallongeIdAsync(tournamentId, respData!.Tournament.Id);
            return;


        }
        else
        {
            Console.WriteLine(JsonConvert.SerializeObject(resp));
        }
    }
}
