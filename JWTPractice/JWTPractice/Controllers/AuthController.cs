using JWTPractice.DTOs;
using JWTPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _authService.RegisterAsync(request);
            if (!success)
            {
                return BadRequest("Username already exists.");
            }

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tokenResponse = await _authService.LogInAsync(request);
            if (tokenResponse == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(tokenResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO request)
        {
            var tokenResponse = await _authService.RefreshTokenAsync(request);
            if (tokenResponse == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            return Ok(tokenResponse);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.FindFirstValue(ClaimTypes.Name);
            return Ok(new { username, name, message = "You are authorized!" });
        }
    }
}
