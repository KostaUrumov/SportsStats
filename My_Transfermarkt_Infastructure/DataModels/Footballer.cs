using SportsStats_Infastructure.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
{
    public class Footballer
    {

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Footballer.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Footballer.MinName)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.Footballer.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Footballer.MinName)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;

        [Required]
        public string AgentId { get; set; } = null!;

        [Required]
        public Position Position { get; set; }

        [Required]
        public Foot PreferedFoot { get; set; }

        [Required]
        [Range(DataConstraints.Footballer.MinCaps, int.MaxValue)]
        public int InternationalCaps { get; set; }

        public int? TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }

        public ICollection<Team> TeamsPlayed { get; set; } = new List<Team>();

        [Required]
        public byte[] Picture { get; set; } = null!;

        [Required]
        [Range(DataConstraints.Footballer.MinValue, double.MaxValue)]

        public decimal CurrentMarketValue { get; set; }

        [Required]
        [Range(DataConstraints.Footballer.MinValue, double.MaxValue)]
        public decimal HighestValue { get; set; }

        [Required]
        public DateTime HishestValueDate { get; set; }

        public DateTime? StartDateContract { get; set; }

        public DateTime? EndDateContract { get; set; }

        public bool IsRetired { get; set; }

        public ICollection<TeamsFootballers> TeamFootballers { get; set; } = new List<TeamsFootballers>();
    }
}
