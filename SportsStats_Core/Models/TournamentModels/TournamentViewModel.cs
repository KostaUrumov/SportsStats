using SportsStats_Core.Models.TeamModels;

namespace SportsStats_Core.Models.TournamentModels
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<ShowTeamModelView> Teams { get; set; } = new List<ShowTeamModelView>();

    }
}
