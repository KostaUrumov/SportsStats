using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class SingleGroupTournament : Tournament
    {
        [Required]
        public int NumberOfTeams { get; set; }

        [Required]
        public int Rounds { get; set; }
    }
}
