using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Models.TeamModels
{
    public class AddNewTeamModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Team.MaxName,
            ErrorMessage = "The {0} must be between {2} and {1}",
            MinimumLength = DataConstraints.Team.MinName)]
        public string Name { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();

        
        
        public int StadiumId { get; set; }

        public IEnumerable<Stadium> Stadiums { get; set; } = new List<Stadium>();
    }
}
