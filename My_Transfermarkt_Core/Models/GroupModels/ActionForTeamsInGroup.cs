using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Models.GroupModels
{
    public class ActionForTeamsInGroup
    {
        public int Id { get; set; }
        public IEnumerable<Team> Teams = new List<Team>();

        public int[]? SelectedTeams { get; set; }
    }
}
