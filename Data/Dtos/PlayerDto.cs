namespace OtmApi.Data.Dtos;

public class PlayerDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Avatar_url { get; set; } = null!;
    public int Global_rank { get; set; }
    public string Country_code { get; set; } = null!;
    public string? DiscordUsername { get; set; }
}

public class PlayerMinDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;

}
public class PlayerMinWithTournamnetsDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public List<TournamentPlayerDto> Tournaments { get; set; } = null!;

}
