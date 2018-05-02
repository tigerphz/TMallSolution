using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Infrastructure.Data
{
    public class PageInfoHelper
    {
        public static void CheckedPageInfo(PageInfo pageInfo, string orderField, SortOrder orderDirection = SortOrder.asc, int pageIndex = 1, int pageSize = 20)
        {
            pageInfo.PageIndex = pageInfo.PageIndex == 0 ? 1 : pageInfo.PageIndex;
            pageInfo.PageSize = pageInfo.PageSize == 0 ? 20 : pageInfo.PageSize;
            pageInfo.OrderField = pageInfo.OrderField ?? orderField;
            pageInfo.OrderDirection = pageInfo.OrderDirection ?? orderDirection;
        }
    }
}
