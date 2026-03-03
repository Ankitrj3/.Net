using LMS.AuthService.DTOs;

namespace LMS.AuthService.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto model);
        Task<string> LoginAsync(LoginDto model);
    }
}
