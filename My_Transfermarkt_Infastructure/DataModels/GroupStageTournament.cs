using System.ComponentModel.DataAnnotations;
namespace My_Transfermarkt_Infastructure.DataModels
{
    public class GroupStageTournament : Tournament
    {
        [Required]
        public int NumberOfTeams { get; set; }

        [Required]
        public int NumberOfGroups { get; set; }
    }
}
