using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
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

        [Required]
        public int CountryID { get; set; }

        [ForeignKey(nameof(CountryID))]
        public Country Country { get; set; } = null!;

        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
