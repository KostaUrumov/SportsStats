namespace My_Transfermarkt_Core.Models.FootballModels
{
    public class ShowFootballerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int InternationalCaps { get; set; }
        public string BirthDay { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Foot { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? CurrentTeam { get; set; }
        public string CurrentValue { get; set; } = null!;
        public string HighestValue { get; set; } = null!;
        public byte[]? Photo { get; set; }
    }
}
