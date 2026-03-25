using JWTPractice.DTOs;

namespace JWTPractice.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDTO request);
        Task<TokenResponseDTO?> LogInAsync(LoginDTO request);
        Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenDTO request);
    }
}
