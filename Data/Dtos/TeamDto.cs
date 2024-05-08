namespace OtmApi.Data.Dtos;

public class TeamDto
{
    public int Id { get; set; }
    public string TeamName { get; set; } = null!;
    public List<PlayerDto>? Players { get; set; }
}
public class TeamWithoutPlayerDto
{
    public int Id { get; set; }
    public string TeamName { get; set; } = null!;
    public bool Isknockout { get; set; }
}