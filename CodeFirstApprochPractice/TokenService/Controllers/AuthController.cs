using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokenService.Services;

namespace TokenService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthController(ITokenService tokenService, IConfiguration config)
        {
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] TokenService.Models.UserDTO request)
        {
            // normally check database using EF Core

            if (request.Username == "admin" && request.Password == "123")
            {
                var token = _tokenService.BuildToken(
                    _config["Jwt:Key"],
                    _config["Jwt:Issuer"],
                    new List<string> { _config["Jwt:Audience"] },
                    request.Username
                );

                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}