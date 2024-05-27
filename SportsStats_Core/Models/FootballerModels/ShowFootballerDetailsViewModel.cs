using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Models.FootballerModels
{
    public class ShowFootballerDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string PrefferedFoot { get; set; } = null!;
        public string Position { get; set; } = null!;
        public ICollection<Team> TeamsPlayed { get; set; } = null!;
        public string HighestValue { get; set; } = null!;
        public DateOnly HighestValueDate { get; set; }
        public int Caps { get; set; }
        public string CurrentValue { get; set; } = null!;
        public string? CurrentTeam { get; set; }
        public byte[] Photo { get; set; } = null!;
        public bool IsRetired { get; set; }
        public string? CurrentUser { get; set; }
    }
}
