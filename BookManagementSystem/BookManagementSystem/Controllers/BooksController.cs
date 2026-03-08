using BookManagementSystem.Models;
using BookManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _service.GetBooks();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _service.GetBookById(id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _service.AddBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _service.GetBookById(id);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
