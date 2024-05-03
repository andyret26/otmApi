namespace OtmApi.Data.Dtos;

public class RoundWithMapsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; }
    public List<MapDto>? Mappool { get; set; }
    public List<MapSuggestionDto>? MapSuggestions { get; set; }
}

public class MapDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string? Image { get; set; }
    public string Mod { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public string Version { get; set; } = null!;
    public decimal Difficulty_rating { get; set; }
    public decimal Bpm { get; set; }
    public decimal Total_length { get; set; }
    public decimal Cs { get; set; }
    public decimal Ar { get; set; }
    public decimal Accuracy { get; set; } // Od
    public string Mapper { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Notes { get; set; }
}

public class MapSuggestionDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string? Image { get; set; }
    public string Mod { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Artist { get; set; } = null!;
    public string Version { get; set; } = null!;
    public decimal Difficulty_rating { get; set; }
    public decimal Bpm { get; set; }
    public decimal Total_length { get; set; }
    public decimal Cs { get; set; }
    public decimal Ar { get; set; }
    public decimal Accuracy { get; set; } // Od
    public string Mapper { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string? Notes { get; set; }
}