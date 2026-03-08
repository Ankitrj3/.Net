using BookManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementSystem.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
