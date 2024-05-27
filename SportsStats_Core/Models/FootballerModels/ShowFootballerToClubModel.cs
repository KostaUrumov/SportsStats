namespace SportsStats_Core.Models.FootballerModels
{
    public class ShowFootballerToClubModel
    {
        public int Id { get; set; }
        public string BirthDay { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int InternationaCaps { get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public byte[]? Picture { get; set; }
        public DateOnly StartContract { get; set; }
        public DateOnly EndContract { get; set; }
        public string Team { get; set; } = null!;

    }
}