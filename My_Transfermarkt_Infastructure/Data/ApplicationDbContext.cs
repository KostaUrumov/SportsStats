using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Transfermarkt_Infastructure.Config;
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
            builder.Entity<AgentsFootballers>()
                .HasKey(k => new { k.AgentId, k.FootballerId });

            builder.Entity<TeamsFootballers>()
                .HasKey(k => new { k.TeamId, k.FootballerId });

            builder.Entity<Footballer>()
                .Property(p => p.CurrentMarketValue)
                .HasPrecision(18,2);

            builder.Entity<Footballer>()
                .Property(p => p.HighestValue)
                .HasPrecision(18, 2);

            builder.Entity<IdentityRole>()
               .HasData(
               new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
               new IdentityRole { Id = "2c93174e-3b0e-446f-86af-883d56fr7210", Name = "User", NormalizedName = "USER".ToUpper() },
               new IdentityRole { Id = "4t67567e-5f7e-446f-88fa-441f56fr8700", Name = "Agent", NormalizedName = "AGENT".ToUpper() });

            builder.ApplyConfiguration(new CountryEntityConfiguration());
            builder.ApplyConfiguration(new TeamEntityConfiguration());

            builder.Entity<My_Transfermarkt_Infastructure.DataModels.Stadium>()
                .HasData(
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 1, CountryId = 5, Build = DateTime.Parse("1999/01/01"), Capacity = 19999, Name = "New Anfield" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 2, CountryId = 77, Build = DateTime.Parse("1899/01/01"), Capacity = 33999, Name = "Arena Koblenz" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 3, CountryId = 80, Build = DateTime.Parse("1999/01/01"), Capacity = 75024, Name = "Alianz Arena" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 4, CountryId = 77, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 5, CountryId = 80, Build = DateTime.Parse("1969/10/06"), Capacity = 24310, Name = "Bochum Arena" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 6, CountryId = 77, Build = DateTime.Parse("1977/01/01"), Capacity = 33979, Name = "Arena Diesel" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 7, CountryId = 80, Build = DateTime.Parse("1948/01/01"), Capacity = 51024, Name = "Solna Arena" },
                    new My_Transfermarkt_Infastructure.DataModels.Stadium { Id = 8, CountryId = 77, Build = DateTime.Parse("1947/05/09"), Capacity = 74667, Name = "Old Trafford" }
                );


            base.OnModelCreating(builder);
        }

        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<AgentsFootballers> AgentsFootballers { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<My_Transfermarkt_Infastructure.DataModels.Stadium> Stadiums { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamsFootballers> TeamsFootballers { get; set; } = null!;
    }
}
