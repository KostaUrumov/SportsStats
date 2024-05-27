using SportsStats_Infastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.TeamModels
{
    public class TeamToAddStadium
    {
        public int Id { get; set; }

        public string? TeamName { get; set; }

        [Required]
        public int StadiumId { get; set; }

        public IEnumerable<Stadium> Stadiums { get; set; } = new List<Stadium>();
    }
}
