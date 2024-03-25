using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.Imports.StadiumImport
{
    public class StadiumImportDTO
    {
        [JsonProperty("Name")]
        [Required]
        [StringLength(maximumLength: DataConstraints.Stadium.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Stadium.MinName)]
        public string Name { get; set; } = null!;

        [JsonProperty("Capacity")]
        [Required]
        public int Capacity { get; set; }

        [JsonProperty("Build")]
        [Required]
        public string BuildDate { get; set; } = null!;

        [JsonProperty("Country")]
        [Required]
        public int CountryId { get; set; }
    }
}
