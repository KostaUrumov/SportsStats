using SportsStats_Infastructure;
using SportsStats_Infastructure.DataModels;
using SportsStats_Infastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.FootballerModels
{
    public class EditFootballerViewModel
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

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();


        public string? AgentId { get; set; }

        [Required]
        public Position Position { get; set; }

        public ICollection<Position> Positions { get; set; } = new List<Position>();

        [Required]
        [Display(Name = "Preffered foot")]
        public Foot PreferedFoot { get; set; }

        public ICollection<Foot> Feet { get; set; } = new List<Foot>();

        [Required]
        [Range(DataConstraints.Footballer.MinCaps, int.MaxValue)]
        public int InternationalCaps { get; set; }


        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new List<Team>();


        public byte[]? Picture { get; set; }

        [Required]
        [RegularExpression(DataConstraints.Footballer.ValueRestriction)]

        public string CurrentMarketValue { get; set; } = null!;


        [Range(DataConstraints.Footballer.MinValue, double.MaxValue)]
        public decimal? HighestValue { get; set; }


        public DateTime? HishestValueDate { get; set; }

        public DateTime? StartDateContract { get; set; }

        public DateTime? EndDateContract { get; set; }

        public ICollection<TeamsFootballers> TeamFootballers { get; set; } = new List<TeamsFootballers>();
    }
}
