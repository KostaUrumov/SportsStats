namespace SportsStats_Core.Models.TournamentModels
{
    public class ShowTournamentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool isGroupTournament { get; set; }
    }
}
