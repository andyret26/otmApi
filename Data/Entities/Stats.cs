namespace OtmApi.Data.Entities;

public class PlayerStats
{
    public int MapId { get; set; }
    public TMap Map { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public int Score { get; set; }
    public decimal Acc { get; set; }
    public List<string>? Mods { get; set; } = null!;

    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;

    public int MatchId { get; set; }
}

public class TeamStats
{
    public int MapId { get; set; }
    public TMap Map { get; set; } = null!;

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public int TotalScore { get; set; }
    public int AvgScore { get; set; }
    public decimal Acc { get; set; }

    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;

    public int MatchId { get; set; }
}