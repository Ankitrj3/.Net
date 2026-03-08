using BookManagementSystem.Data;
using BookManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        //public static List<Book> books = new List<Book>()
        //{
        //    new Book { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m },
        //    new Book { BookId = 2, Title = "To Kill a Mocking", Author = "Harper Lee", Price = 12.99m }
        //};
        private readonly MyDbContext _books;
        public BookRepository(MyDbContext books)
        {
            _books = books;
        }
        public async Task AddBook(Book book)
        {
            if(_books.Books.Any(b => b.BookId == book.BookId))
            {
                throw new Exception("Book with the same ID already exists.");
            }
            _books.Books.Add(book);
            _books.SaveChanges();
        }

        public async Task DeleteBook(int id)
        {
            var book = _books.Books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                _books.Books.Remove(book);
                _books.SaveChanges();
            }
            throw new Exception("Book not found");
        }

        public async Task<IActionResult> GetBookById(int id)
        {
            var book = _books.Books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                return new OkObjectResult(book);
            }
            return new NotFoundResult();
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return _books.Books.ToList();
        }
    }
}
