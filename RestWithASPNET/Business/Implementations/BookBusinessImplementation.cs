using System.Collections.Generic;
using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Utils;
using RestWithASPNET.Model;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness    
    {
        public IRepository<Book> _repository;
        public IBookRepository _bookRepository;
        private BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository, IBookRepository bookRepository)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(int id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public BookVO Disabled(long id)
        {
            return _converter.Parse(_bookRepository.Disabled(id));
        }

        public List<BookVO> FindByAuthorOrTitle(string author, string title)
        {
            return _converter.Parse(_bookRepository.FindByAuthorOrTitle(author, title));
        }

        public PagedSearchVO<BookVO> FindWithPagedSearch
            (string author, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) 
                && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from books b where 1 = 1 ";
            if(!string.IsNullOrWhiteSpace(author)) query += $" and b.author like '%{author}%' ";
            query += $" order by b.author {sort} limit {size} offset {offset}";

            string countQuery = @"select count(*) from books b where 1 = 1 ";
            if(!string.IsNullOrWhiteSpace(author)) countQuery += $" and b.author like '%{author}%' ";

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO> 
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }
    }
}