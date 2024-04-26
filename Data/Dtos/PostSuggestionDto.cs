namespace OtmApi.Data.Dtos;

public class PostSuggestionDto
{
    public int TournamentId { get; set; }
    public int RoundId { get; set; }
    public int MapId { get; set; }
    public string Mod { get; set; } = null!;
    public string? Notes { get; set; }
}