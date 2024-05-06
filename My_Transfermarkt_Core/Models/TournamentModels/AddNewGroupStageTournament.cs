namespace My_Transfermarkt_Core.Models.TournamentModels
{
    public class AddNewGroupStageTournament : AddNewSingleGroupTournamentModel
    {
        public int NumberOfGroups { get; set; }

        public int RoundsPerGroup { get; set; }

        public int TeamsNumberInGroup { get; set; }
    }
}
