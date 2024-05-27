using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.FootballerModels
{
    public class SearchByCountryModel
    {
        [Required]
        public string Country { get; set; } = null!;
    }
}
