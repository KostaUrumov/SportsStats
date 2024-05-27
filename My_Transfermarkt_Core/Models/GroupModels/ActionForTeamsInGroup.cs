using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Models.GroupModels
{
    public class ActionForTeamsInGroup
    {
        public int Id { get; set; }
        public IEnumerable<Team> Teams = new List<Team>();

        public int[]? SelectedTeams { get; set; }
    }
}
