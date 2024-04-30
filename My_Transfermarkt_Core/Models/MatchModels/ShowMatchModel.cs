namespace My_Transfermarkt_Core.Models.MatchModels
{
    public class ShowMatchModel
    {
        public int TournamentId { get; set; }
        public string HomeTeam { get; set; } = null!;
        public string AwayTeam { get; set; } = null!;
        public string Result { get; set; } = null!;
        public string Date { get; set; } = null!;
    }
}
