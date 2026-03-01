using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpenseManagementWithMvcAndEntityFramework.Data
{
    public class MyExpenseContextFactory : IDesignTimeDbContextFactory<MyExpenseContext>
    {
        public MyExpenseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MyExpenseContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new MyExpenseContext(optionsBuilder.Options);
        }
    }
}
