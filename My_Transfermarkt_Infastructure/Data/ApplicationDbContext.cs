using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgentsFootballers>()
                .HasKey(k => new { k.AgentId, k.FootballerId });

            builder.Entity<StadiumsTeams>()
                .HasKey(k => new { k.StadiumId, k.TeamId });

            builder.Entity<TeamsFootballers>()
                .HasKey(k => new { k.TeamId, k.FootballerId });

            builder.Entity<Footballer>()
                .Property(p => p.CurrentMarketValue)
                .HasPrecision(18,2);

            builder.Entity<Footballer>()
                .Property(p => p.HighestValue)
                .HasPrecision(18, 2);


            base.OnModelCreating(builder);
        }

        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<AgentsFootballers> AgentsFootballers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Stadium> Stadiums { get; set; } = null!;
        public DbSet<StadiumsTeams> StadiumsTeams { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamsFootballers> TeamsFootballers { get; set; } = null!;
    }
}
