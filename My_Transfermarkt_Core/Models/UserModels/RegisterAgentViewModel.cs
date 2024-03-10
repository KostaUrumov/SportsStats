using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.UserModels
{
    public class RegisterAgentViewModel : RegisterUserViewModel
    {
        [Required]
        public string Agent { get; set; } = null!;
    }
}
