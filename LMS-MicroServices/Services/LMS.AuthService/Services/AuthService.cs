using LMS.AuthService.DTOs;
using LMS.AuthService.Exceptions;
using LMS.AuthService.Model;
using LMS.AuthService.Repositories;

namespace LMS.AuthService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> RegisterAsync(RegisterDto model)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new AuthenticationException("Email already registered");
            }

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth
            };
            var result = await _userRepository.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                throw new AuthenticationException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            await _userRepository.AddToRoleAsync(user, model.Role);
            return "User Registered Successfully";
        }
        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userRepository.GetEmailAsync(model.Email);
            if (user == null)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            var valid = await _userRepository.CheckPasswordAsync(user, model.Password);
            if (!valid)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            var roles = await _userRepository.GetRolesAsync(user);
            return _tokenService.CreateToken(user, roles);
        }
    }
}
