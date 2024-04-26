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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stats>()
            .HasKey(s => new { s.MapId, s.PlayerId, s.RoundId });

        modelBuilder.Entity<Host>()
            .HasMany(h => h.Tournaments)
            .WithOne(t => t.Host)
            .HasForeignKey(t => t.HostId);



        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Staff)
            .WithMany(s => s.Tournaments)
            .UsingEntity(j => j.ToTable("TournamentStaff"));

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Players)
            .WithMany(p => p.Tournaments)
            .UsingEntity(j => j.ToTable("TournamentPlayer"));

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