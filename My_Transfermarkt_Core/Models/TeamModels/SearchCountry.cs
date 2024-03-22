using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.TeamModels
{
    public class SearchCountry
    {
        [Required]
        public string Country { get; set; } = null!;
    }
}
