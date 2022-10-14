using System.Collections.Generic;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHyperMedia
    {
        public PagedSearchVO() { }

        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.SortFields = sortFields;
            this.SortDirections = sortDirections;
            Filters = filters;
        }
        public PagedSearchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.SortFields = sortFields;
            this.SortDirections = sortDirections;

        }
        public PagedSearchVO(int currentPage, int pageSize, int totalResults, string sortFields, string sortDirections) 
        : this (currentPage, 10, sortFields, sortDirections) {  }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public List<T> List { get; set; }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}