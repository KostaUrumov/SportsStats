using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Transfermarkt_Infastructure.Configuration;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationAgentsFootballersConfiguration());
            builder.ApplyConfiguration(new ApplicationTournamentsTeamsConfiguration());
            builder.ApplyConfiguration(new ApplicationTeamsFootballersConfiguration());
            builder.ApplyConfiguration(new ApplicationIdentityRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationCountryConfiguration());
            builder.ApplyConfiguration(new ApplicationStadiumConfiguration());
            builder.ApplyConfiguration(new ApplicationTournamentConfiguration());
            builder.ApplyConfiguration(new ApplicationTeamConfiguration());
            builder.ApplyConfiguration(new ApplicationFootballerConfiguration());

            base.OnModelCreating(builder);
        }



        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<AgentsFootballers> AgentsFootballers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<TournamentsTeams> TournamentsTeams { get; set; } = null!;
        public DbSet<My_Transfermarkt_Infastructure.DataModels.Stadium> Stadiums { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamsFootballers> TeamsFootballers { get; set; } = null!;
    }
}
