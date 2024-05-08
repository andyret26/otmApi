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
    public List<TeamWithoutPlayerDto>? Teams { get; set; }
    public List<PlayerDto>? Players { get; set; }

    public StaffDto? Referee { get; set; }
    public string Num { get; set; } = null!;
}