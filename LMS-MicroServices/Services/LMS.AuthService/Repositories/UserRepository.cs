using LMS.AuthService.Model;
using Microsoft.AspNetCore.Identity;

namespace LMS.AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<ApplicationUser?> GetEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
            => await _userManager.GetRolesAsync(user);

        public async Task AddToRoleAsync(ApplicationUser user, string role)
            => await _userManager.AddToRoleAsync(user, role);
    }
}
