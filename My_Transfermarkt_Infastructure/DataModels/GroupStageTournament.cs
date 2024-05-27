using System.ComponentModel.DataAnnotations;
namespace SportsStats_Infastructure.DataModels
{
    public class GroupStageTournament : Tournament
    {
        [Required]
        [Range(DataConstraints.GroupStageTournament.MinNumberOfTeams, DataConstraints.GroupStageTournament.MaxNumberOfTeams)]
        public int NumberOfTeams { get; set; }

        [Required]
        [Range(DataConstraints.GroupStageTournament.MinGroupsNumber, DataConstraints.GroupStageTournament.MaxGroupsNumber)]
        public int NumberOfGroups { get; set; }

        public ICollection<GroupsTournament> Groups = new List<GroupsTournament>();
    }
}
