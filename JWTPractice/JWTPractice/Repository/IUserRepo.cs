using JWTPractice.Models;
using System.Threading.Tasks;

namespace JWTPractice.Repository
{
    public interface IUserRepo
    {
        Task<User> RegisterUserAsync(User user);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserAsync(User user);
    }
}
