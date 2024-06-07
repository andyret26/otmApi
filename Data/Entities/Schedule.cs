using System.ComponentModel.DataAnnotations;

namespace OtmApi.Data.Entities;

public class Schedule
{
    [Key]
    public int Id { get; set; }
    public int Num { get; set; }
    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;
    public int RoundNumber { get; set; }
    public DateTime DateTime { get; set; }
    public string? Referee { get; set; }
    public string? Streamer { get; set; }
    public List<string>? Commentators { get; set; }
    public string? Name1 { get; set; }
    public string? Name2 { get; set; }

    public int? Score1 { get; set; }
    public int? Score2 { get; set; }
    public string? Winner { get; set; }
    public string? Loser { get; set; }

    public int? MpLinkId { get; set; }
}
public class QualsSchedule
{
    [Key]
    public int Id { get; set; }
    public int RoundId { get; set; }
    public Round Round { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public List<string>? Names { get; set; }

    public string? Referee { get; set; }
    public string Num { get; set; } = null!;

    public int? MatchId { get; set; }
}