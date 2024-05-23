using My_Transfermarkt_Core.Models.TeamModels;

namespace My_Transfermarkt_Core.Models.TournamentModels
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<ShowTeamModelView> Teams { get; set; } = new List<ShowTeamModelView>();

    }
}
