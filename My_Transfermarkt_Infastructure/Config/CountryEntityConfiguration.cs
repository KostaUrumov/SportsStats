using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;
using My_Transfermarkt_Infastructure.Imports.CountryImport;
using Newtonsoft.Json;

namespace My_Transfermarkt_Infastructure.Config
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            string jsonText = File.ReadAllText("C:\\Users\\Kostadin Urumov\\Desktop\\My_Transfermarkt\\My_Transfermarkt_Infastructure\\Config\\JsonSeedFiles\\ConfigCountries.json");


            CountryImportDTO[] newCountries = JsonConvert.DeserializeObject<CountryImportDTO[]>( jsonText);
            for (int i = 0; i< newCountries.Count(); i++)
            {
                builder.HasData
                    (
                        new Country { Id = i + 1, Name = newCountries[i].CountryName, ShortName = newCountries[i].CountryCode }
                    );
            }

        }
    }
}
