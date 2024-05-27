using System.ComponentModel.DataAnnotations;

namespace SportsStats_Core.Models.UserModels
{
    public class ChangePassWordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string RepeatNewPassword { get; set; } = null!;

        public string? UserId { get; set; }
    }
}
