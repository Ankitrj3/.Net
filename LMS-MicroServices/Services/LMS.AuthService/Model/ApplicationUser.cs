using Microsoft.AspNetCore.Identity;

namespace LMS.AuthService.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateOnly DateOfBirth {  get; set; }
    }
}
