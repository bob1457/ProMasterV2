using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProMaster.Infrastructure.Utilities
{
    interface IPagedList
    {
        int TotalCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool HasPreviousPage {get;}
        bool HasNextPage { get; } 
    
    }
}
