using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Agent
    {
        [Required]
        public string Id { get; set; } = null!;

        [ForeignKey(nameof(Id))]
        public User User { get; set; } = null!;

        public ICollection<AgentsFootballers> AgentFootballers { get; set; } = new List<AgentsFootballers>();
    }
}
