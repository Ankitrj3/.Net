using BookManagementSystem.Models;

namespace BookManagementSystem.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task DeleteBook(int id);
        Task AddBook(Book book);
    }
}
