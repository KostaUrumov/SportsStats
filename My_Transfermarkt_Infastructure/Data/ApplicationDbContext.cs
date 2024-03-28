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

            builder.Entity<Stadium>()
                .HasData(
                    new Stadium { Id = 1, CountryId = 5, Build = DateTime.Parse("1999/01/01"), Capacity = 19999, Name = "New Anfield" },
                    new Stadium { Id = 2, CountryId = 77, Build = DateTime.Parse("1899/01/01"), Capacity = 33999, Name = "Arena Koblenz" },
                    new Stadium { Id = 3, CountryId = 80, Build = DateTime.Parse("1999/01/01"), Capacity = 75024, Name = "Alianz Arena" },
                    new Stadium { Id = 4, CountryId = 77, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
                    new Stadium { Id = 5, CountryId = 80, Build = DateTime.Parse("1969/10/06"), Capacity = 24310, Name = "Bochum Arena" },
                    new Stadium { Id = 6, CountryId = 77, Build = DateTime.Parse("1977/01/01"), Capacity = 33979, Name = "Arena Diesel" },
                    new Stadium { Id = 7, CountryId = 80, Build = DateTime.Parse("1948/01/01"), Capacity = 51024, Name = "Solna Arena" },
                    new Stadium { Id = 8, CountryId = 77, Build = DateTime.Parse("1947/05/09"), Capacity = 74667, Name = "Old Trafford" },
                    new Stadium { Id = 9, CountryId = 77, Build = DateTime.Parse("1974/02/04"), Capacity = 81365, Name = "Westfalenstadion" },
                    new Stadium { Id = 10, CountryId = 77, Build = DateTime.Parse("2004/11/17"), Capacity = 81365, Name = "Red Bull Arena" },
                    new Stadium { Id = 11, CountryId = 77, Build = DateTime.Parse("1928/06/04"), Capacity = 50000, Name = "Max-Morlock-Stadion" },
                    new Stadium { Id = 12, CountryId = 77, Build = DateTime.Parse("1929/11/08"), Capacity = 60449, Name = "MHPArena" },
                    new Stadium { Id = 13, CountryId = 34, Build = DateTime.Parse("2023/04/30"), Capacity = 18455, Name = "Hristo Botev" },
                    new Stadium { Id = 14, CountryId = 34, Build = DateTime.Parse("1963/03/10"), Capacity = 29200, Name = "Georgi Asparihov" }
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
