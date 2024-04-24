using System.ComponentModel.DataAnnotations;

namespace OtmApi.Data.Entities;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? FormuPostLink { get; set; }
    public bool IsTeamTourney { get; set; }
    public string Format { get; set; } = null!;
    public int MaxTeamSize { get; set; }
    public string RankRange { get; set; } = null!;



    public List<Round>? Rounds { get; set; }
    public List<Player>? Players { get; set; }
    public List<Team>? Teams { get; set; }
    public List<Staff>? Staff { get; set; }

    public int HostId { get; set; }
    public Host Host { get; set; } = null!;
}

public class Team
{
    public int Id { get; set; }
    public string TeamName { get; set; } = null!;
    public List<Player>? Players { get; set; }
}