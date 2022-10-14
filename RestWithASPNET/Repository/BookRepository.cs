using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Context;
using RestWithASPNET.Model;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(MySqlContext context) : base(context)
        {
        }

        public Book Disabled(long id)
        {
            if(!_context.Books.Any(b => b.Id.Equals(id))) return null;
            var book = _context.Books.SingleOrDefault(u => u.Id.Equals(id));

            if(book is not null)
            {
                book.Enabled = false;
                try
                {
                    _context.Entry(book).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }

            return book;
        }

        public List<Book> FindByAuthorOrTitle(string author, string title)
        {
            if(!string.IsNullOrWhiteSpace(author) && !string.IsNullOrEmpty(title))
            {
                return _context.Books.Where(b => 
                        b.Author.Contains(author) 
                        && b.Title.Contains(title))
                        .ToList();
            }
            else if(!string.IsNullOrWhiteSpace(author) && string.IsNullOrEmpty(title))
            {
                 return _context.Books.Where(b => 
                        b.Author.Contains(author))
                        .ToList();
            }
            else if(string.IsNullOrWhiteSpace(author) && !string.IsNullOrEmpty(title))
            {
                 return _context.Books.Where(b => 
                        b.Title.Contains(title))
                        .ToList();
            }
            return null;
        }
    }
}