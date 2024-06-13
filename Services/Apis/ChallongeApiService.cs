
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

    public async Task AddParticipantsBulkAsync(int tournamentId)
    {
        if (API_KEY == null) throw new Exception("CHALLONGE_API_KEY environment variable not set");
        var t = await _tourneyService.GetByIdSimpleAsync(tournamentId);

        var names = new List<string>();
        var seeds = new List<int>();
        var ids = new List<int>();

        if (await _tourneyService.IsTeamTourneyAsync(tournamentId))
        {
            var teams = (await _tourneyService.GetAllTeamsAsync(tournamentId)).Where(t => t.Seed != null).OrderBy(t => t.Seed);
            foreach (var team in teams)
            {
                names.Add(team.TeamName.Replace(" ", "%20"));
                seeds.Add((int)team.Seed!);
                ids.Add(team.Id);
            }
        }
        else
        {
            var players = (await _tourneyService.GetAllTournamentPlayersAsync(tournamentId)).Where(p => p.Seed != null).OrderBy(p => p.Seed);
            foreach (var p in players)
            {
                names.Add(p.Player.Username.Replace(" ", "%20"));
                seeds.Add((int)p.Seed!);
                ids.Add(p.PlayerId);
            }
        }

        var http = new HttpClient();
        var parameters = $"?api_key={API_KEY}";

        for (var i = 0; i < names.Count; i++)
        {
            parameters += $"&participants[][name]={names[i]}&participants[][seed]={seeds[i]}&participants[][misc]={ids[i]}";
        };

        var fullUrl = BASE_URL + $"/tournaments/{t.ChallongeId}/participants/bulk_add.json" + parameters;

        var resp = await http.PostAsync(fullUrl, null);

        if (resp.IsSuccessStatusCode)
        {

            return;
        }
        else
        {
            System.Console.WriteLine(await resp.Content.ReadAsStringAsync());
            throw new Exception("Failed to add participants to challonge");
        }


    }
}
