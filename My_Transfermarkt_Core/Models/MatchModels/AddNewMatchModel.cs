using My_Transfermarkt_Infastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.MatchModels
{
    public class AddNewMatchModel
    {
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int Round { get; set; }

        public List<int> Rounds { get; set; } = new List<int>();

        public int? GroupId { get; set; }

        [Required]
        public int TournamentId { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int? RefereeId { get; set; }

        public IEnumerable<Referee> Referees { get; set; } = new List<Referee>();
        public IEnumerable<Team> Teams { get; set; } = new List<Team>();
    }
}
