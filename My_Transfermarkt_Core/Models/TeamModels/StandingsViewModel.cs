using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Models.TeamModels
{
    public class StandingsViewModel
    {
        public Team Team { get; set; } = null!;
        public string TeamName { get; set; } = null!;
        public byte[] Picture { get; set; } = null!;

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int Points { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int PlusMinusGoals { get; set; }
    }
}
