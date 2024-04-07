using System.ComponentModel.DataAnnotations;

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
    }
}
