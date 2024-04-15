using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationTeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                 .HasData
                 (
                     new Team { Id = 1, Name = "Manchester United", CountryId = 8, StadiumId = 8 },
                     new Team { Id = 2, Name = "Newcastle United", CountryId = 8, StadiumId = 2 },
                     new Team { Id = 3, Name = "Hertha Berlin", CountryId = 14, StadiumId = 4 },
                     new Team { Id = 4, Name = "Kolenz", CountryId = 14, StadiumId = 2 },
                     new Team { Id = 5, Name = "AIK Solna", CountryId = 15, StadiumId = 7 },
                     new Team { Id = 6, Name = "Lecester City", CountryId = 8, StadiumId = 1 },
                     new Team { Id = 7, Name = "Liverpool", CountryId = 8, StadiumId = 1 },
                     new Team { Id = 8, Name = "Stuttgart", CountryId = 14, StadiumId = 12 },
                     new Team { Id = 9, Name = "Borissia Dortmund", CountryId = 14, StadiumId = 9 },
                     new Team { Id = 12, Name = "Levski Sofia", CountryId = 1, StadiumId = 14 },
                     new Team { Id = 14, Name = "Bochum", CountryId = 14, StadiumId = 5 },
                     new Team { Id = 17, Name = "Bayern Munchen", CountryId = 14, StadiumId = 3 },
                     new Team { Id = 31, Name = "Botev Plovdiv", CountryId = 1, StadiumId = 13 },
                     new Team { Id = 51, Name = "Nuremberg", CountryId = 14, StadiumId = 11 },
                     new Team { Id = 61, Name = "FC Magdeburg", CountryId = 14, StadiumId = 6 },
                     new Team { Id = 82, Name = "BFC Dynamo", CountryId = 14, StadiumId = 4 }
                 );
        }
    }
}
