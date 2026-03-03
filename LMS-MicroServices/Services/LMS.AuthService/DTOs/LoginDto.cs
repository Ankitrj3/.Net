using System.ComponentModel.DataAnnotations;

namespace LMS.AuthService.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Invalid email or password")]
        public string Password { get; set; } = string.Empty;
    }
}
