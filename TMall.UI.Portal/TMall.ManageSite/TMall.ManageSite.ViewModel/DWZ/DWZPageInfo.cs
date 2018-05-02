using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.ManageSite.ViewModel
{
    public class DWZPageInfo
    {
        public int? pageNum { get; set; }

        public int? numPerPage { get; set; }

        public string orderField { get; set; }

        public string orderDirection { get; set; }
    }
}
