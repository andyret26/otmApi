namespace OtmApi.Data.Dtos;

public class GenQualsRequestDto
{
    public int TournamentId { get; set; }
    public int RoundId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class QualsScheduleDto
{
    public int Id { get; set; }
    public int RoundId { get; set; }
    public DateTime DateTime { get; set; }
    public List<string>? Names { get; set; }

    public string? Referee { get; set; }
    public string Num { get; set; } = null!;

    public int? MatchId { get; set; }
}

public class QualsSchedulePutDto
{
    public int TourneyId { get; set; }
    public int RoundId { get; set; }
    public int ScheduleId { get; set; }
    public int? RefId { get; set; }
    public List<string>? Names { get; set; }
    public int? MpLinkId { get; set; }
}

public class QualsScheduleAddExtraDto
{
    public int TourneyId { get; set; }
    public int RoundId { get; set; }
    public string Num { get; set; } = null!;
    public DateTime DateTime { get; set; }
}


public class ScheduleDto
{
    public int Id { get; set; }
    public int Num { get; set; }
    public int RoundId { get; set; }
    public RoundDto Round { get; set; } = null!;
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
    public bool IsInLosersBracket { get; set; }

    public int? MpLinkId { get; set; }
}