using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.TeamModels
{
    public class SearchCountry
    {
        [Required]
        public string Country { get; set; } = null!;
    }
}
