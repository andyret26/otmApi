namespace OtmApi.Data.Entities;
public class Player
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string? DiscordUsername { get; set; }
    public string Avatar_url { get; set; } = null!;
    public int Global_rank { get; set; }
    public string Country_code { get; set; } = null!;
    public List<TournamentPlayer>? Tournaments { get; set; }
    public List<Team>? Teams { get; set; }
    public List<Stats>? Stats { get; set; }

}