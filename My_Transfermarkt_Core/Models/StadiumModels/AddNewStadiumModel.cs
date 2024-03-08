using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Models.StadiumModels
{
    public class AddNewStadiumModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Stadium.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Stadium.MinName)]
        public string Name { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        [Required]

        public DateTime Build { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
    }
}
