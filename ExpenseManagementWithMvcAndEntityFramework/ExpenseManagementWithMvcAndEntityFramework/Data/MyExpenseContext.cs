using ExpenseManagementWithMvcAndEntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagementWithMvcAndEntityFramework.Data
{
    public class MyExpenseContext : DbContext
    {
        public MyExpenseContext(DbContextOptions<MyExpenseContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
    }
}
