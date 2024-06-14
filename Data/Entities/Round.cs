using System.ComponentModel.DataAnnotations;

namespace OtmApi.Data.Entities;

public class Round
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsQualifier { get; set; } = false;
    public List<TMap>? Mappool { get; set; }
    public List<TMapSuggestion>? MapSuggestions { get; set; }
    public List<Schedule> Schedules { get; set; } = null!;
    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; } = null!;
    public bool IsMpLinksPublic { get; set; } = false;
    public bool IsStatsPublic { get; set; } = false;
    public int RoundNumber { get; set; }
}

public class TMap
{
    [Key]
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

    public List<Round>? Rounds { get; set; }
    public List<PlayerStats>? PlayerStats { get; set; }
    public List<TeamStats>? TeamStats { get; set; }

}
public class TMapSuggestion
{
    [Key]
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

    public List<Round>? Rounds { get; set; }

}