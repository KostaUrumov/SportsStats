using My_Transfermarkt_Infastructure.DataModels;
using My_Transfermarkt_Infastructure.Enums;
using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.FootballerModels
{
    public class AddNewFootallerModel
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
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();

       
        public string? AgentId { get; set; }

        [Required]
        public Position Position { get; set; }

        public ICollection<Position> Positions { get; set; } = new List<Position>();

        [Required]
        [Display(Name ="Preffered foot")]
        public Foot PreferedFoot { get; set; }

        public ICollection<Foot> Feet { get; set; } = new List<Foot>();

        [Required]
        [Range(DataConstraints.Footballer.MinCaps, int.MaxValue)]
        public int InternationalCaps { get; set; }

        
        public int? TeamId { get; set; }

        public ICollection<Team> Teams { get; set; } = new List<Team>();


        [Required]
        [RegularExpression(DataConstraints.Footballer.ValueRestriction)]
        public string CurrentMarketValue { get; set; } = null!;


        public ICollection<TeamsFootballers> TeamFootballers { get; set; } = new List<TeamsFootballers>();

    }
}
