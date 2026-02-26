using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AccountController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Product");

            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("AuthService");
                var loginRequest = new { Username = model.Username, Password = model.Password };
                var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var authResponse = JsonSerializer.Deserialize<AuthApiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (response.IsSuccessStatusCode && authResponse?.Success == true && !string.IsNullOrEmpty(authResponse.Token))
                {
                    await SignInUserAsync(authResponse);
                    TempData["SuccessMessage"] = $"Welcome back, {authResponse.Username}!";
                    return RedirectToLocal(model.ReturnUrl);
                }

                ModelState.AddModelError(string.Empty, authResponse?.Message ?? "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to connect to authentication service: {ex.Message}");
            }

            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Product");

            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("AuthService");
                var registerRequest = new
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    FullName = model.FullName
                };
                var content = new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Auth/register", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var authResponse = JsonSerializer.Deserialize<AuthApiResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (response.IsSuccessStatusCode && authResponse?.Success == true && !string.IsNullOrEmpty(authResponse.Token))
                {
                    await SignInUserAsync(authResponse);
                    TempData["SuccessMessage"] = $"Welcome, {authResponse.Username}! Your account has been created.";
                    return RedirectToLocal(model.ReturnUrl);
                }

                ModelState.AddModelError(string.Empty, authResponse?.Message ?? "Registration failed.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to connect to authentication service: {ex.Message}");
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUserAsync(AuthApiResponse authResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authResponse.Username ?? ""),
                new Claim(ClaimTypes.Email, authResponse.Email ?? ""),
                new Claim(ClaimTypes.Role, authResponse.Role ?? "User"),
                new Claim("jwt_token", authResponse.Token ?? "")
            };

            // Parse JWT token for additional claims
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(authResponse.Token))
            {
                var jwtToken = handler.ReadJwtToken(authResponse.Token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Product");
        }
    }
}
