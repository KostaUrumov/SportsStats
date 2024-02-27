using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static My_Transfermarkt_Infastructure.DataConstraints;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class AgentsFootballers
    {
        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public Agent Agent { get; set; } = null!;


        [Required]
        public int FootballerId { get; set; }

        [ForeignKey(nameof(FootballerId))]
        public Footballer Footballer { get; set; } = null!;

    }
}
