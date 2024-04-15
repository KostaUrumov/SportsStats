using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Infastructure.Configuration
{
    public class ApplicationStadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            builder
                .HasData(
                    new Stadium { Id = 1, CountryId = 8, Build = DateTime.Parse("1999/01/01"), Capacity = 19999, Name = "New Anfield" },
                    new Stadium { Id = 2, CountryId = 14, Build = DateTime.Parse("1899/01/01"), Capacity = 33999, Name = "Arena Koblenz" },
                    new Stadium { Id = 3, CountryId = 14, Build = DateTime.Parse("1999/01/01"), Capacity = 75024, Name = "Alianz Arena" },
                    new Stadium { Id = 4, CountryId = 14, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
                    new Stadium { Id = 5, CountryId = 14, Build = DateTime.Parse("1969/10/06"), Capacity = 24310, Name = "Bochum Arena" },
                    new Stadium { Id = 6, CountryId = 14, Build = DateTime.Parse("1977/01/01"), Capacity = 33979, Name = "Arena Diesel" },
                    new Stadium { Id = 7, CountryId = 15, Build = DateTime.Parse("1948/01/01"), Capacity = 51024, Name = "Solna Arena" },
                    new Stadium { Id = 8, CountryId = 8, Build = DateTime.Parse("1947/05/09"), Capacity = 74667, Name = "Old Trafford" },
                    new Stadium { Id = 9, CountryId = 14, Build = DateTime.Parse("1974/02/04"), Capacity = 81365, Name = "Westfalenstadion" },
                    new Stadium { Id = 10, CountryId = 14, Build = DateTime.Parse("2004/11/17"), Capacity = 81365, Name = "Red Bull Arena" },
                    new Stadium { Id = 11, CountryId = 14, Build = DateTime.Parse("1928/06/04"), Capacity = 50000, Name = "Max-Morlock-Stadion" },
                    new Stadium { Id = 12, CountryId = 14, Build = DateTime.Parse("1929/11/08"), Capacity = 60449, Name = "MHPArena" },
                    new Stadium { Id = 13, CountryId = 1, Build = DateTime.Parse("2023/04/30"), Capacity = 18455, Name = "Hristo Botev" },
                    new Stadium { Id = 14, CountryId = 1, Build = DateTime.Parse("1963/03/10"), Capacity = 29200, Name = "Georgi Asparihov" }
                );
        }
    }
}
