using System.ComponentModel.DataAnnotations;

namespace OtmApi.Data.Entities;

public class Schedule
{
    [Key]
    public int Id { get; set; }
    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public Team? Team1 { get; set; }
    public Team? Team2 { get; set; }
    public Player? Player1 { get; set; }
    public Player? Player2 { get; set; }
}
public class QualsSchedule
{
    [Key]
    public int Id { get; set; }
    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public List<Team>? Teams { get; set; }
    public List<Player>? Players { get; set; }

    public Staff? Referee { get; set; }
    public string Num { get; set; } = null!;
}