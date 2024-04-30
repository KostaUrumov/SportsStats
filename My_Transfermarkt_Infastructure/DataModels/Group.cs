using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        public int? TournamentID { get; set; }

        [ForeignKey(nameof(TournamentID))]
        public Tournament Tournament { get; set; } = null!;

        public ICollection<GroupTeams> Teams { get; set; } = new List<GroupTeams>();
    }
}
