using BookManagementSystem.Models;
using BookManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task AddBook(Book book)
        {
            await _bookRepository.AddBook(book);
        }
        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }
        public async Task<Book> GetBookById(int id)
        {
            var result = await _bookRepository.GetBookById(id);
            if (result is OkObjectResult okResult)
            {
                return okResult.Value as Book;
            }
            throw new Exception("Book not found.");
        }
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.GetBooks();
        }
    }
}