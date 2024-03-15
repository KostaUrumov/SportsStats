namespace My_Transfermarkt_Core.Models.FootballerModels
{
    public class ShowFootballerDetailsViewModel
    {
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PrefferedFoot { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string[] TeamsPlayed { get; set; } = null!;
        public string HighestValue { get; set; } = null!;
        public DateOnly HighestValueDate { get; set; } 
        public string CurrentValue { get; set; } = null!;
    }
}
