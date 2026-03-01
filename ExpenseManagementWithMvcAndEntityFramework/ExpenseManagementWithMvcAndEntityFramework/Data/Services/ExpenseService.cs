using ExpenseManagementWithMvcAndEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagementWithMvcAndEntityFramework.Data.Services
{
    public class ExpenseService : IExpenseService
    {
        public readonly MyExpenseContext _context;
        public ExpenseService(MyExpenseContext context)
        {
            _context = context;
        }

        public async Task Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var find = await _context.Expenses.FindAsync(id);
            if (find != null)
            {
                _context.Expenses.Remove(find);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Edit(Expense expense)
        {
            var find = await _context.Expenses.FindAsync(expense.Id);
            if(find != null)
            {
                find.Name = expense.Name;
                find.Category = expense.Category;
                find.Description = expense.Description;
                find.Amount = expense.Amount;
                find.Date = expense.Date;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            var expense = await _context.Expenses.ToListAsync();
            return expense;
        }

        public async Task<Expense?> GetById(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public IQueryable GetChartData()
        {
            var data = _context.Expenses
                              .GroupBy(e => e.Category)
                              .Select(g => new
                              {
                                  Category = g.Key,
                                  Total = g.Sum(e => e.Amount)
                              });
            return data;
        }
    }
}
