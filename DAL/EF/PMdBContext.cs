using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PM.BL.Domain;

namespace PM.DAL.EF;

public class PMdBContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Host> Hosts { get; set; }
    public DbSet<EpisodeParticipation> EpisodeParticipations { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }

    public PMdBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=PM.db");
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }

    public bool CreateDatabase(bool dropDatabase)
    {
        
        if (dropDatabase)
        {
            Database.EnsureDeleted();
        }
        return Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<EpisodeParticipation>().HasKey(ep => ep.Id); 
        modelBuilder.Entity<EpisodeParticipation>().HasOne(ep => ep.Episode).WithMany(e => e.GuestsOnEpisode).IsRequired();
        modelBuilder.Entity<EpisodeParticipation>().HasOne(ep => ep.Guest).WithMany(g => g.EpisodeParticipations);
        modelBuilder.Entity<EpisodeParticipation>().HasOne(ep => ep.Sponsor).WithMany(s => s.MentionedOnEpisodes).HasForeignKey("FK_SponsorID").IsRequired();
        
        // modelBuilder.Entity<Sponsor>().HasMany(s => s.MentionedOnEpisodes).WithOne(ep => ep.Sponsor);

        modelBuilder.Entity<Host>().HasMany(h => h.Episodes).WithOne(e => e.Host);
    }
}