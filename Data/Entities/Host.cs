namespace OtmApi.Data.Entities;

public class Host
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;

    public List<Tournament>? Tournaments { get; set; }

}
