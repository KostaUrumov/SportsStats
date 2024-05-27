using SportsStats_Core.Models.TeamModels;

namespace SportsStats_Core.Models.TournamentModels
{
    public class AddTeamsToTournament
    {
        public int Id { get; set; }
        public List<ShowTeamModelView> Teams = new List<ShowTeamModelView>();

        public int[]? SelectedTeams { get; set; }
    }
}
