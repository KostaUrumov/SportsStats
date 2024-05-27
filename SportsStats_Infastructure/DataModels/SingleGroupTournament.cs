using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Infastructure.DataModels
{
    public class SingleGroupTournament : Tournament
    {
        [Required]
        [Range(DataConstraints.GroupStageTournament.MinNumberOfTeams, DataConstraints.GroupStageTournament.MaxNumberOfTeams)]
        public int NumberOfTeams { get; set; }

        [Required]
        public int Rounds { get; set; }
    }
}
