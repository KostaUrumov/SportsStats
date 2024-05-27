using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
{
    public class RefereesTournaments
    {
        [Required]
        public int TournamentId { get; set; }

        [ForeignKey(nameof(TournamentId))]
        public Tournament Tournament { get; set; } = null!;

        [Required]
        public int RefereeId { get; set; }

        [ForeignKey(nameof(RefereeId))]
        public Referee Referee { get; set; } = null!;
    }
}
