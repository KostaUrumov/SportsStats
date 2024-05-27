using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Infastructure.DataModels
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Tournament.MaxTournamentName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Tournament.MinTournamentName)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public ICollection<Referee> Referees { get; set; } = new List<Referee>();
        public ICollection<TournamentsTeams> TeamsTournaments { get; set; } = new List<TournamentsTeams>();


    }
}
