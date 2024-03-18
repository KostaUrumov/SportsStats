using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Transfermarkt_Infastructure.DataModels;
using My_Transfermarkt_Infastructure.Imports.TeamImports;
using Newtonsoft.Json;

namespace My_Transfermarkt_Infastructure.Config
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            string json = File.ReadAllText("C:\\Users\\Kostadin Urumov\\Desktop\\My_Transfermarkt\\My_Transfermarkt_Infastructure\\Config\\JsonSeedFiles\\Teams.json");
            TeamImportDTO[] teams = JsonConvert.DeserializeObject<TeamImportDTO[]>(json);
            for (int i = 0; i< teams.Count(); i++)
            {
                builder.HasData
                    (
                      new Team {Id = i+1, CountryId = int.Parse(teams[i].CountryId.ToString()), Name = teams[i].Name }
                    );
            }
            
        }
    }
}
