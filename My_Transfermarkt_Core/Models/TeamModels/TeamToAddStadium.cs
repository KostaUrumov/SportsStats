using My_Transfermarkt_Infastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.TeamModels
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
