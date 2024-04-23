namespace OtmApi.Data.Dtos;

public class PostTourneyDto
{
    public string Name { get; set; } = null!;
    public string? FormuPostLink { get; set; }

    public bool IsTeamTourney { get; set; }
    public string Format { get; set; } = null!;
    public int MaxTeamSize { get; set; }
    public string RankRange { get; set; } = null!;
}