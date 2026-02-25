using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
namespace MyWebApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options){ }

        public DbSet<Item> Items { get; set; }
    }
}
