using LMS.AuthService.Model;

namespace LMS.AuthService.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
    }
}
