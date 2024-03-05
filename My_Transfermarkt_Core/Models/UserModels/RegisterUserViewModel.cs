using My_Transfermarkt_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.UserModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxName,
           ErrorMessage = "The {0} must be between {2} and {1}",
           MinimumLength = DataConstraints.User.MinName)]
        [RegularExpression(DataConstraints.User.ExampleName, ErrorMessage = "{0} example: Kostadin")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxName,
           ErrorMessage = "The {0} must be between {2} and {1}",
           MinimumLength = DataConstraints.User.MinName)]
        [RegularExpression(DataConstraints.User.ExampleName, ErrorMessage = "{0} example: Urumov")]
        [Display(Name = "Username")]

        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(DataConstraints.User.ExamplePhone, ErrorMessage = "{0} example: 0888888888")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(maximumLength: DataConstraints.User.MaxUserName,
            ErrorMessage = "The {0} must be between {2} and {1}",
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
