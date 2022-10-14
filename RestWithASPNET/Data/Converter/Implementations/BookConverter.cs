using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Data.Converter.Contract;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if(origin is null) return null;

            return new Book
            {
                Id = origin.Id,
                Author = origin.Author,
                launchDate = origin.launchDate,
                Price = origin.Price,
                Title = origin.Title,
                Enabled = origin.Enabled
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if(origin is null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public BookVO Parse(Book origin)
        {
            if(origin is null) return null;

            return new BookVO
            {
                Id = (int)origin.Id,
                Author = origin.Author,
                launchDate = origin.launchDate,
                Price = origin.Price,
                Title = origin.Title,
                Enabled = origin.Enabled
            };
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if(origin is null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}