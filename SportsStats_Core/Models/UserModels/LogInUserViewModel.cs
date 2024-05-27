using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.UserModels
{
    public class LogInUserViewModel
    {
        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxUserName,
            ErrorMessage = "The {0} must be between {2} and {1}",
           MinimumLength = DataConstraints.User.MinUserName)]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
