using LMS.AuthService.Model;
using Microsoft.AspNetCore.Identity;

namespace LMS.AuthService.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync (ApplicationUser user, string password);
        Task<ApplicationUser?> GetEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task AddToRoleAsync(ApplicationUser user, string role);
    }
}
