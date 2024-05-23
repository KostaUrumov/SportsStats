namespace My_Transfermarkt_Core.Models.GeneralModels
{
    public class ResultsViewModel
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Id { get; set; }
        public string? Team { get; set; }
    }
}
