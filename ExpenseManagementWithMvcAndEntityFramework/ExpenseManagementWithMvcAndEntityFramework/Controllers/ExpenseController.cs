using ExpenseManagementWithMvcAndEntityFramework.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementWithMvcAndEntityFramework.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _service;
        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var expense = await _service.GetExpenses();
            return View(expense);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Models.Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _service.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _service.Edit(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public async Task<IActionResult> Details(int id)
        {
            var expense = await _service.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _service.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetChart()
        {
            var data = _service.GetChartData();
            return Json(data);
        }
    }
}
