using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.TournamentModels
{
    public class AddNewTournamentModel
    {
        [Required]
        [StringLength(maximumLength: DataConstraints.Tournament.MaxTournamentName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Tournament.MinTournamentName)]
        public string Name { get; set; } = null!;

    }
}
