using My_Transfermarkt_Infastructure;
using My_Transfermarkt_Infastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.RefereeModels
{
    public class AddNewRefereeModel
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
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
    }
}
