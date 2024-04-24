namespace My_Transfermarkt_Core.Models.RefereeModels
{
    public class ShowRefereeModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Rating { get; set; }
        public string Country { get; set; } = null!;
    }
}
