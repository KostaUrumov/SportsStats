using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.UserModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxName,
           ErrorMessage = "The {0} must be between {1} and {2}",
           MinimumLength = DataConstraints.User.MinName)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxName,
           ErrorMessage = "The {0} must be between {1} and {2}",
           MinimumLength = DataConstraints.User.MinName)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression("[0]{1}[0-9]{9}")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxUserName,
            ErrorMessage = "The {0} must be between {1} and {2}",
           MinimumLength = DataConstraints.User.MinUserName)]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
