using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.CountryModels
{
    public class AddNewCountryModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: DataConstraints.Country.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Country.MinName)]
        [RegularExpression(DataConstraints.Country.Name, ErrorMessage = "Example: Bulgaria")]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression(DataConstraints.Country.ShortName, ErrorMessage = "Example: BUL")]
        public string ShortName { get; set; } = null!;
    }
}
