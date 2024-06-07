namespace OtmApi.Data.Dtos;

public class RoundDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; }
    public bool IsMpLinksPublic { get; set; }
    public bool IsStatsPublic { get; set; }
    public int TournamentId { get; set; }
}

public class RoundPostDto
{
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; }
}


public class RoundDetaildDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; }
    public List<MapWithStatsDto>? Mappool { get; set; }
    public List<MapSuggestionDto>? MapSuggestions { get; set; }
    public bool IsMpLinksPublic { get; set; }
    public bool IsStatsPublic { get; set; }
    public TournamentSimpleDto Tournament { get; set; } = null!;
}

