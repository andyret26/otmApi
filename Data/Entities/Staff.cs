namespace OtmApi.Data.Entities;

public class Staff
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public List<string> Roles { get; set; } = null!;
    public List<Tournament> Tournaments { get; set; } = null!;
}
