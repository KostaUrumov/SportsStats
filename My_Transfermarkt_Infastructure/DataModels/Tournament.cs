using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Tournament.MaxTournamentName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Tournament.MinTournamentName)]
        public string Name { get; set; } = null!;

        public ICollection<TournamentsTeams> TeamsTournaments { get; set; } = new List<TournamentsTeams>();

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public ICollection<TournamentsTeams> Teams { get; set; } = new List<TournamentsTeams>();

        public ICollection<Referee> Referees { get; set; } = new List<Referee>();
        
    }
}
