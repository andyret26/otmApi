namespace OtmApi.Data.Dtos;

public class PostSuggestionDto
{
    public int MapId { get; set; }
    public string Mod { get; set; } = null!;
    public string? Notes { get; set; }
}