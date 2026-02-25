using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;
using System.Diagnostics;
using System.Linq;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expense()
        {
            return View(ExpenseStore.Expenses);   //send list to view
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id == null)
                return View(new Expense());

            var expense = ExpenseStore.Expenses.FirstOrDefault(x => x.Id == id);
            return View(expense);
        }

        [HttpPost]
        public IActionResult CreateEditExpense(Expense expense)
        {
            if (!ModelState.IsValid)
                return View(expense);

            if (expense.Id == 0) // CREATE
            {
                expense.Id = ExpenseStore.Expenses.Count == 0
                    ? 1
                    : ExpenseStore.Expenses.Max(x => x.Id) + 1;

                ExpenseStore.Expenses.Add(expense);
            }
            else // UPDATE
            {
                var existing = ExpenseStore.Expenses.FirstOrDefault(x => x.Id == expense.Id);
                if (existing != null)
                {
                    existing.Value = expense.Value;
                    existing.Description = expense.Description;
                }
            }

            return RedirectToAction("Expense");
        }

        public IActionResult Delete(int id)
        {
            var expense = ExpenseStore.Expenses.FirstOrDefault(x => x.Id == id);
            if (expense != null)
                ExpenseStore.Expenses.Remove(expense);

            return RedirectToAction("Expense");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}