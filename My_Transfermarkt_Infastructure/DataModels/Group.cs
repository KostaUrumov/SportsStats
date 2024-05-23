using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int TournamentID { get; set; }

        [ForeignKey(nameof(TournamentID))]
        public Tournament Tournament { get; set; } = null!;

        [Required]
        [Range(DataConstraints.Group.MinNumOfRounds, DataConstraints.Group.MaxNumberOfRounds)]
        public int NumberOfRounds { get; set; }

        [Required]
        [Range(DataConstraints.Group.MinNumTeams, DataConstraints.Group.MaxNumTeams)]
        public int TeamsNumber { get; set; }

        public ICollection<GroupTeams> Teams { get; set; } = new List<GroupTeams>();
    }
}
