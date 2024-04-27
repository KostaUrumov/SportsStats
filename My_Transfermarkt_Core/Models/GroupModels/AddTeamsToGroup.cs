using My_Transfermarkt_Core.Models.TeamModels;

namespace My_Transfermarkt_Core.Models.GroupModels
{
    public class AddTeamsToGroup
    {
        public int GroupId { get; set; }
        public List<ShowTeamModelView> Teams = new List<ShowTeamModelView>();

        public int[]? SelectedTeams { get; set; }
    }
}
