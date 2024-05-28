namespace OtmApi.Data.Dtos;

public class PlayerStatsDto
{
    public int MapId { get; set; }

    public int PlayerId { get; set; }
    public PlayerMinWithTournamnetsDto Player { get; set; } = null!;

    public int Score { get; set; }
    public decimal Acc { get; set; }
    public List<string>? Mods { get; set; } = null!;

    public int RoundId { get; set; }
}

public class TeamStatsDto
{
    public int MapId { get; set; }

    public int TeamId { get; set; }
    public TeamWithoutPlayerDto Team { get; set; } = null!;

    public int TotalScore { get; set; }
    public int AvgScore { get; set; }
    public decimal Acc { get; set; }

    public int RoundId { get; set; }

}
