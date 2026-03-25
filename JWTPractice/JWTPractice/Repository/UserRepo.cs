using JWTPractice.Data;
using JWTPractice.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JWTPractice.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly MyDbContext _context;

        public UserRepo(MyDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.username == username);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
