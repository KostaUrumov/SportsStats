using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Models.GroupModels
{
    public class DetailsGroupModel
    {
        public Team Team { get; set; } = null!;
        public string TeamName { get; set; } = null!;
        public byte[]? Picture { get; set; }

        public int GoalsFor { get; set;}

        public int GoalsAgainst { get; set; }

        public int Points { get; set; }
    }
}
