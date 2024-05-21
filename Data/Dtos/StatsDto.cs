namespace OtmApi.Data.Dtos;

public class StatsDto
{
    public int MapId { get; set; }
    public MapDto Map { get; set; } = null!;

    public int PlayerId { get; set; }
    public PlayerDto Player { get; set; } = null!;

    public int Score { get; set; }
    public decimal Acc { get; set; }
    public List<string>? Mods { get; set; } = null!;

    public int RoundId { get; set; }
    public RoundDto Round { get; set; } = null!;
}