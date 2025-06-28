//using GraphQLApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new();
        private int _idCounter = 1;

        public Book AddBook(Book book)
        {
            book.Id = _idCounter++;
            _books.Add(book);
            return book;
        }

        public IEnumerable<Book> GetAllBooks() => _books;

        public Book? GetBookById(int id) => 
            _books.FirstOrDefault(b => b.Id == id);

        public Task<IEnumerable<Book>> GetBooksByIds(IEnumerable<int> ids)
        {
            var idList = ids.ToList();
            var result = _books.Where(b => idList.Contains(b.Id));
            return Task.FromResult(result);
        }

        public Book? UpdateBook(Book book)
        {
            var existing = GetBookById(book.Id);
            if (existing == null) return null;
            
            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Genre = book.Genre;
            existing.PublishedYear = book.PublishedYear;
            return existing;
        }

        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null) return false;
            return _books.Remove(book);
        }
    }
}