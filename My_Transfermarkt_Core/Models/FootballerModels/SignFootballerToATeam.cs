using My_Transfermarkt_Infastructure.DataModels;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.FootballerModels
{
    public class SignFootballerToATeam
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TeamId { get; set; }

        public ICollection<Team> Teams = new List<Team>();

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartContractDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndContractDate { get; set; }
    }
}
