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
    public bool MpLinkIsVisable { get; set; }
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