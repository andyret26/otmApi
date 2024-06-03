namespace OtmApi.Data.Dtos;

public class TournamentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int HostId { get; set; }
    public string? FormuPostLink { get; set; }
    public bool IsTeamTourney { get; set; }
    public string Format { get; set; } = null!;
    public int MaxTeamSize { get; set; }
    public string RankRange { get; set; } = null!;

    public List<RoundDto>? Rounds { get; set; }
    public List<TournamentPlayerDto>? Players { get; set; }
    public List<TeamDto>? Teams { get; set; }
    public List<StaffDto>? Staff { get; set; }
}
public class TournamentSimpleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? FormuPostLink { get; set; }
    public bool IsTeamTourney { get; set; }
    public string Format { get; set; } = null!;
    public int MaxTeamSize { get; set; }
    public string RankRange { get; set; } = null!;
    public string? HowManyQualifies { get; set; }
}

public class StaffDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public List<string> Roles { get; set; } = null!;
    public int TournamentId { get; set; }
}


public class TournamentPlayerDto
{
    public int PlayerId { get; set; }
    public int TournamentId { get; set; }
    public bool Isknockout { get; set; } = false;
}

public class IsStaffRequest
{
    public int TournamentId { get; set; }
    public string[] Roles { get; set; } = null!;
}