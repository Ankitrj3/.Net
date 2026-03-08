using BookManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IActionResult> GetBookById(int id);
        Task DeleteBook(int id);
        Task AddBook(Book book);
    }
}
