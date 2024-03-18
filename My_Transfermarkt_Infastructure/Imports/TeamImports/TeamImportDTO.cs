using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.Imports.TeamImports
{
    public class TeamImportDTO
    {
        [JsonProperty("name")]
        [StringLength(maximumLength: DataConstraints.Team.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Team.MinName)]
        public string Name { get; set; } = null!;

        [JsonProperty("country")]
        public int CountryId { get; set; }
    }
}
