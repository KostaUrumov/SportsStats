using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationTournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder
               .HasData
               (
                   new Tournament { Id = 1, Name = "Champions League 23/24" },
                   new Tournament { Id = 2, Name = "Bundesliga 23/24" },
                   new Tournament { Id = 3, Name = "Serie A 23/24" },
                   new Tournament { Id = 4, Name = "Championship 23/24" },
                   new Tournament { Id = 5, Name = "Parva Liga 23/24" },
                   new Tournament { Id = 7, Name = "Premier League 23/24" }
               );
        }
    }
}
