using EntityFrameworkCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

        }
        public DbSet<Student> Students { get; set; }
    }
}
