namespace SportsStats_Core.Models.FootballerModels
{
    public class AllFootballersViewModel : ShowFootballerDetailsViewModel
    {
        public string Agent { get; set; } = null!;
        public int Id { get; set; }

        public string AgentName { get; set; } = null!;
    }
}
