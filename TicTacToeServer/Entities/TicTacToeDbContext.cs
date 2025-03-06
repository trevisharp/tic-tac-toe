using Microsoft.EntityFrameworkCore;

namespace TicTacToeServer.Entities;

public class TicTacToeDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Match>()
            .HasOne(m => m.Player1)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.Entity<Match>()
            .HasOne(m => m.Player2)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}