using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.Infrastructure.SearchModel;

namespace TMall.Infrastructure.Data
{
    public class SearchPageInfo<TData> where TData : class
    {
        /// <summary>
        /// 页信息
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public SearchCondition SearchCondition { get; set; }

        /// <summary>
        /// 获取到的数据集合
        /// </summary>
        public List<TData> DataList { get; set; }
    }
}
