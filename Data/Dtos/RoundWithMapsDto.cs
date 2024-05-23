namespace OtmApi.Data.Dtos;

public class RoundWithMapsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; }
    public List<MapDto>? Mappool { get; set; }
    public List<MapSuggestionDto>? MapSuggestions { get; set; }
    public bool IsMpLinksPublic { get; set; }
    public bool IsStatsPublic { get; set; }
}

