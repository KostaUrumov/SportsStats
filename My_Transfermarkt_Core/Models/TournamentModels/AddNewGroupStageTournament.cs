using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.TournamentModels
{
    public class AddNewGroupStageTournament : AddNewSingleGroupTournamentModel
    {
        [Required]
        [Display(Name = "Groups")]
        public int NumberOfGroups { get; set; }

        [Required]
        public int RoundsPerGroup { get; set; }

        public int TeamsNumberInGroup { get; set; }
    }
}
