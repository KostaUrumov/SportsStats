using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Infastructure.Configuration
{
    public class ApplicationCountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasData
                (
                    new Country { Id = 1, Name = "Bulgaria", ShortName = "BU" },
                    new Country { Id = 2, Name = "Spain", ShortName = "SP" },
                    new Country { Id = 3, Name = "Hungary", ShortName = "HU" },
                    new Country { Id = 4, Name = "Usa", ShortName = "US" },
                    new Country { Id = 5, Name = "Japan", ShortName = "JA" },
                    new Country { Id = 6, Name = "Bahrain", ShortName = "BA" },
                    new Country { Id = 7, Name = "Ukraine", ShortName = "UK" },
                    new Country { Id = 8, Name = "England", ShortName = "EN" },
                    new Country { Id = 9, Name = "France", ShortName = "FR" },
                    new Country { Id = 10, Name = "Norway", ShortName = "NO" },
                    new Country { Id = 11, Name = "Egypt", ShortName = "EG" },
                    new Country { Id = 12, Name = "Netherlands", ShortName = "NE" },
                    new Country { Id = 13, Name = "Italy", ShortName = "IT" },
                    new Country { Id = 14, Name = "Germany", ShortName = "DE" },
                    new Country { Id = 15, Name = "Sweden", ShortName = "SW" }
                );
        }
    }
}
