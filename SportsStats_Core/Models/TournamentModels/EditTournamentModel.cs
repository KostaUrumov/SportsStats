using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.TournamentModels
{
    public class EditTournamentModel
    {
        [Required]
        [StringLength(maximumLength: DataConstraints.Tournament.MaxTournamentName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Tournament.MinTournamentName)]
        public string Name { get; set; } = null!;

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndtDate { get; set; }

        [Required]
        public int NumberOfTeams { get; set; }

        public int Rounds { get; set; }

        public int Groups { get; set; }
    }
}
