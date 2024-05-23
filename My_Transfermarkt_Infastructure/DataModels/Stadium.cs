using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Stadium
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Stadium.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Stadium.MinName)]
        public string Name { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        [Required]
        public DateTime Build { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;

    }
}
