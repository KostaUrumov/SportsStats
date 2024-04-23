using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Referee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Referee.MaxNameSymbols,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Referee.MinNameSymbols)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.Referee.MaxNameSymbols,
           ErrorMessage = "The {0} must be between {1} and {2}",
           MinimumLength = DataConstraints.Referee.MinNameSymbols)]
        public string LastName { get; set; } = null!;

        [Range(DataConstraints.Referee.MinRating, DataConstraints.Referee.Maxrating)]
        public double? Rating { get; set; }
    }
}
