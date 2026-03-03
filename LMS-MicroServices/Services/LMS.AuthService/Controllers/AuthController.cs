using LMS.AuthService.Common;
using LMS.AuthService.DTOs;
using LMS.AuthService.Exceptions;
using LMS.AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMS.AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Data = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)))
                    });
                }

                var result = await _authService.RegisterAsync(model);
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = result
                });
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred"
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Data = string.Join("; ", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)))
                    });
                }

                var token = await _authService.LoginAsync(model);
                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Login Successful",
                    Data = token
                });
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>
                {
                    Success = false,
                    Message = "An unexpected error occurred"
                });
            }
        }
    }
}
