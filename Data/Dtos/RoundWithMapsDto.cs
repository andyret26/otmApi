namespace OtmApi.Data.Dtos;

public class RoundWithMapsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
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
    public decimal Sr { get; set; }
    public int Bpm { get; set; }
    public decimal Length { get; set; }
    public decimal Cs { get; set; }
    public decimal Ar { get; set; }
    public decimal Od { get; set; }
    public string Mapper { get; set; } = null!;
    public string? Notes { get; set; }
    public string? Link { get; set; }
}

public class MapSuggestionDto
{
    public int Id { get; set; }
    public int OrderNumber { get; set; }
    public string? Image { get; set; }
    public string Mod { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Sr { get; set; }
    public int Bpm { get; set; }
    public decimal Length { get; set; }
    public decimal Cs { get; set; }
    public decimal Ar { get; set; }
    public decimal Od { get; set; }
    public string Mapper { get; set; } = null!;
    public string? Notes { get; set; }
    public string? Link { get; set; }
}