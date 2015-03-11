using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ProMaster.Infrastructure.Utilities
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPage { get; private set; }

        public PaginatedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPage = (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));

        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex + 1 > 1);
                //return (PageIndex > 0);
            }
        }

        public bool HasNextPage
        {
            get 
            { 
                //return (PageIndex < TotalPage);
                return (PageIndex + 1 < TotalPage); 
            }
        }
    }

    public static class Pagination
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int index, int pageSize)
        { 
            return new PaginatedList<T>(source, index, pageSize);
        }

        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int index)
        {
            return new PaginatedList<T>(source, index, 10);
        }

    }
}
