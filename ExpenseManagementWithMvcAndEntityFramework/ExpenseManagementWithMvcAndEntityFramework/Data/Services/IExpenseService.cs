using ExpenseManagementWithMvcAndEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
namespace ExpenseManagementWithMvcAndEntityFramework.Data.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetExpenses();
        Task<Expense?> GetById(int id);
        Task Add(Expense expense);
        Task Edit(Expense expense);
        Task Delete(int id);
        IQueryable GetChartData();
    }
}
