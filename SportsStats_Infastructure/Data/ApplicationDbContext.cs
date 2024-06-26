﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsStats_Infastructure.Configuration;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationTournamentsTeamsConfiguration());
            builder.ApplyConfiguration(new ApplicationTeamsFootballersConfiguration());
            builder.ApplyConfiguration(new ApplicationIdentityRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationCountryConfiguration());
            builder.ApplyConfiguration(new ApplicationStadiumConfiguration());
            builder.ApplyConfiguration(new ApplicationRefereesTournamentsConfiguration());
            builder.ApplyConfiguration(new ApplicationMatchConfiguration());
            builder.ApplyConfiguration(new ApplicationGroupTeamsConfiguration());
            builder.ApplyConfiguration(new ApplicationGroupsTournamentConfiguration());

            base.OnModelCreating(builder);
        }



        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<GroupTeams> GroupsTeams { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<GroupStageTournament> GroupStageTournaments { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Referee> Referees { get; set; } = null!;
        public DbSet<RefereesTournaments> RefereesTournaments { get; set; } = null!;
        public DbSet<SingleGroupTournament> SingleGroupTournaments { get; set; } = null!;
        public DbSet<Stadium> Stadiums { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamsFootballers> TeamsFootballers { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<TournamentsTeams> TournamentsTeams { get; set; } = null!;
        public DbSet<GroupsTournament> GroupsTournaments { get; set; } = null!;


    }
}
