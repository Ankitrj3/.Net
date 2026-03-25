using JWTPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTPractice.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
