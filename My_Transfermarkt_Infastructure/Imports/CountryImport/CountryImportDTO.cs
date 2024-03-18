using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.Imports.CountryImport
{
    public class CountryImportDTO
    {
        [JsonProperty("name")]
        [StringLength(maximumLength: DataConstraints.Country.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Country.MinName)]
        [RegularExpression(DataConstraints.Country.Name)]
        public string CountryName { get; set; } = null!;

        [JsonProperty("code")]
        [RegularExpression(DataConstraints.Country.ShortName)]
        public string CountryCode { get; set; } = null!;
    }
}
