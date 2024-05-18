namespace My_Transfermarkt_Core.Models.MatchModels
{
    public class ShowMatchModel
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public string HomeTeam { get; set; } = null!;
        public string AwayTeam { get; set; } = null!;
        public string Result { get; set; } = null!;
        public string Date { get; set; } = null!;
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public  int Round { get; set; }
        public byte[] HomeLogo { get; set; } = null!;
        public byte[] AwayLogo { get; set; } = null!;
    }
}
