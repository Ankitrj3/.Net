using System.ComponentModel.DataAnnotations;

namespace LMS.AuthService.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain uppercase, lowercase, digit, and special character")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = string.Empty;
    }
}
