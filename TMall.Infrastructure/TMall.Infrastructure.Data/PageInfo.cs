using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Infrastructure.Data
{
    public class PageInfo
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderField { get; set; }

        public SortOrder? OrderDirection { get; set; }

        public int TotalCount { get; set; }
    }
}
