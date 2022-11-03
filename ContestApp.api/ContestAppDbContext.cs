using ContestApp.api.Distances;
using ContestApp.api.Players;
using ContestApp.api.Tournaments;
using Microsoft.EntityFrameworkCore;

namespace ContestApp.api;

public class ContestAppDbContext : DbContext
{
    public ContestAppDbContext(DbContextOptions options):base(options)
    {
        
    }

    public DbSet<Player> Players { get; set; }

    public DbSet<Tournament> Tournaments { get; set; }

    public DbSet<Distance> Distances { get; set; }
}