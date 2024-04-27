using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationSingleGroupTournamentConfiguration : IEntityTypeConfiguration<SingleGroupTournament>
    {
        public void Configure(EntityTypeBuilder<SingleGroupTournament> builder)
        {
            builder
               .HasData
               (
                   new SingleGroupTournament { Id = 1, Name = "Champions League 23/24" },
                   new SingleGroupTournament { Id = 2, Name = "Bundesliga 23/24" },
                   new SingleGroupTournament { Id = 3, Name = "Serie A 23/24" },
                   new SingleGroupTournament { Id = 4, Name = "Championship 23/24" },
                   new SingleGroupTournament { Id = 5, Name = "Parva Liga 23/24" },
                   new SingleGroupTournament { Id = 7, Name = "Premier League 23/24" }
               );
        }
    }
}
