using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Infastructure.DataModels
{
    public class User : IdentityUser
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
    }
}
