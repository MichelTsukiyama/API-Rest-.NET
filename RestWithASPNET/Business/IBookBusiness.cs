using System.Collections.Generic;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Utils;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface IBookBusiness
    {
        List<BookVO> FindAll();
        BookVO FindById(int id);
        List<BookVO> FindByAuthorOrTitle(string author, string title);
        PagedSearchVO<BookVO> FindWithPagedSearch(string author, string sortDirection, int pageSize, int page);
        
        BookVO Create(BookVO book);
        BookVO Update(BookVO book);
        void Delete(int id);
        BookVO Disabled(long id);
    }
}