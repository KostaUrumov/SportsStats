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
            builder.ApplyConfiguration(new ApplicationSingleGroupTournamentConfiguration());
            builder.ApplyConfiguration(new ApplicationTeamConfiguration());
            builder.ApplyConfiguration(new ApplicationFootballerConfiguration());
            builder.ApplyConfiguration(new ApplicationRefereesTournamentsConfiguration());
            builder.ApplyConfiguration(new ApplicationMatchConfiguration());
            

            base.OnModelCreating(builder);
        }



        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<AgentsFootballers> AgentsFootballers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<GroupStageTournament> GroupStageTournaments { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Referee> Referees { get; set; } = null!;
        public DbSet<RefereesTournaments> RefereesTournaments { get; set; } = null!;
        public DbSet<SingleGroupTournament> SingleGroupTournaments { get; set; } = null!;
        public DbSet<My_Transfermarkt_Infastructure.DataModels.Stadium> Stadiums { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamsFootballers> TeamsFootballers { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<TournamentsTeams> TournamentsTeams { get; set; } = null!;
        
        
       
        
        
    }
}
