using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<User?> GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }

    public class AuthenticationService : IAuthService
    {
        private readonly IMongoCollection<User> _users;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IOptions<MongoDbSettings> mongoSettings, IOptions<JwtSettings> jwtSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _users = database.GetCollection<User>(mongoSettings.Value.UsersCollectionName);
            _jwtSettings = jwtSettings.Value;

            // Create unique indexes
            var indexKeysDefinition = Builders<User>.IndexKeys;
            _users.Indexes.CreateOne(new CreateIndexModel<User>(
                indexKeysDefinition.Ascending(u => u.Username),
                new CreateIndexOptions { Unique = true }));
            _users.Indexes.CreateOne(new CreateIndexModel<User>(
                indexKeysDefinition.Ascending(u => u.Email),
                new CreateIndexOptions { Unique = true }));
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if username already exists
            var existingUser = await _users.Find(u => u.Username == request.Username).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Username already exists"
                };
            }

            // Check if email already exists
            existingUser = await _users.Find(u => u.Email == request.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Email already registered"
                };
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _users.InsertOneAsync(user);

            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Registration successful",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _users.Find(u => u.Username == request.Username).FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            if (!user.IsActive)
            {
                return new AuthResponse
                {
                    Success = false,
                    Message = "Account is deactivated"
                };
            }

            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("fullName", user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
