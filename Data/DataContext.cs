using Microsoft.EntityFrameworkCore;
using OtmApi.Data.Entities;
using Host = OtmApi.Data.Entities.Host;

namespace OtmApi.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<Host> Hosts { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Staff> Staff { get; set; } = null!;
    public DbSet<Tournament> Tournaments { get; set; } = null!;
    public DbSet<Round> Rounds { get; set; } = null!;
    public DbSet<TMap> Maps { get; set; } = null!;
    public DbSet<TMapSuggestion> MapSuggestions { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<QualsSchedule> QualsSchedules { get; set; } = null!;

    public DbSet<TournamentPlayer> TournamentPlayer { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stats>()
            .HasKey(s => new { s.MapId, s.PlayerId, s.RoundId });

        modelBuilder.Entity<Staff>()
            .HasKey(s => new { s.Id, s.TournamentId });

        modelBuilder.Entity<Host>()
            .HasMany(h => h.Tournaments)
            .WithOne(t => t.Host)
            .HasForeignKey(t => t.HostId);



        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Staff)
            .WithOne(s => s.Tournament);


        modelBuilder.Entity<TournamentPlayer>()
            .HasKey(tp => new { tp.PlayerId, tp.TournamentId });

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
            .WithMany(p => p.Teams)
            .UsingEntity(j => j.ToTable("TeamPlayer"));

        modelBuilder.Entity<TMap>()
            .HasKey(m => new { m.Id, m.Mod });

        modelBuilder.Entity<TMapSuggestion>()
            .HasKey(m => new { m.Id, m.Mod });

        modelBuilder.Entity<TMap>()
            .HasMany(m => m.Rounds)
            .WithMany(r => r.Mappool)
            .UsingEntity(j => j.ToTable("RoundMap"));

        modelBuilder.Entity<TMapSuggestion>()
            .HasMany(m => m.Rounds)
            .WithMany(r => r.MapSuggestions)
            .UsingEntity(j => j.ToTable("RoundMapSuggestion"));





        base.OnModelCreating(modelBuilder);
    }
}