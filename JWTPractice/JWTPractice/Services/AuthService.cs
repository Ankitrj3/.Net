using JWTPractice.DTOs;
using JWTPractice.Helper;
using JWTPractice.Models;
using JWTPractice.Repository;
using System.Security.Claims;

namespace JWTPractice.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        private readonly IJwtHelper _jwtHelper;

        public AuthService(IUserRepo userRepo, IJwtHelper jwtHelper)
        {
            _userRepo = userRepo;
            _jwtHelper = jwtHelper;
        }

        public async Task<bool> RegisterAsync(RegisterDTO request)
        {
            var existingUser = await _userRepo.GetUserByUsernameAsync(request.username);
            if (existingUser != null)
            {
                return false;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);

            var newUser = new User
            {
                name = request.name,
                age = request.age,
                username = request.username,
                password = passwordHash,

                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            await _userRepo.RegisterUserAsync(newUser);
            return true;
        }

        public async Task<TokenResponseDTO?> LogInAsync(LoginDTO request)
        {
            var user = await _userRepo.GetUserByUsernameAsync(request.username);
            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, user.password))
            {
                return null;
            }

            var token = _jwtHelper.GenerateJwtToken(user);
            var refreshToken = _jwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepo.UpdateUserAsync(user);

            return new TokenResponseDTO
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenDTO request)
        {
            // Find the user by their refresh token string directly
            var user = await _userRepo.GetUserByRefreshTokenAsync(request.RefreshToken);

            // Check if user exists and token has not expired
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            // Generate a fresh pair of tokens
            var newAccessToken = _jwtHelper.GenerateJwtToken(user);
            var newRefreshToken = _jwtHelper.GenerateRefreshToken();

            // Save the new refresh token (for security - rotation)
            user.RefreshToken = newRefreshToken;
            await _userRepo.UpdateUserAsync(user);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
