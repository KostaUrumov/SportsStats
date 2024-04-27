using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime MatchDate { get; set; }

        [Required]
        public int TournamentId { get; set; }

        [ForeignKey(nameof(TournamentId))]
        public Tournament Tournament { get; set; } = null!;
        
        public int? RefereeId { get; set; }

        [ForeignKey(nameof(RefereeId))]
        public Referee? Referee { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public virtual  Team HomeTeam { get; set; } = null!;

        [Required]
        public int AwayTeamId { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public virtual  Team AwayTeam { get; set; } = null!;

        [Range(DataConstraints.Match.MinScore,DataConstraints.Match.MaxScore)]
        public int? HomeScore { get; set; }

        [Range(DataConstraints.Match.MinScore, DataConstraints.Match.MaxScore)]
        public int? AwayScore { get; set; }
    }
}
