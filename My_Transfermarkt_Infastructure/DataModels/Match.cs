using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime MatchDate { get; set; }

        [Required]
        public int TournamentId { get; set; }

        [ForeignKey(nameof(TournamentId))]
        public Tournament Tournament { get; set; } = null!;

        public int? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group? Group { get; set; }

        public int? RefereeId { get; set; }

        [ForeignKey(nameof(RefereeId))]
        public Referee? Referee { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; } = null!;

        [Required]
        public int StadiumId { get; set; }

        [ForeignKey(nameof(StadiumId))]
        public virtual Stadium Stadium { get; set; } = null!;

        [Required]
        public int Attendace { get; set; }

        public ICollection<Footballer> HomeStartes { get; set; } = new List<Footballer>();

        public ICollection<Footballer> AwayStartes { get; set; } = new List<Footballer>();

        public ICollection<Footballer> HomeSubs { get; set; } = new List<Footballer>();

        public ICollection<Footballer> AwaySubs { get; set; } = new List<Footballer>();



        [Required]
        public int Round { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; } = null!;

        [Range(DataConstraints.Match.MinScore, DataConstraints.Match.MaxScore)]
        public int? HomeScore { get; set; }

        [Range(DataConstraints.Match.MinScore, DataConstraints.Match.MaxScore)]
        public int? AwayScore { get; set; }
    }
}
