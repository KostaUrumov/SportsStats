using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Country.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Country.MinName)]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression("[A - Z]{3}")]
        public string ShortName { get; set; } = null!;
    }
}
