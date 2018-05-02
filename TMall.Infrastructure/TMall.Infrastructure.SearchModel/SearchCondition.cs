using System.Collections.Generic;

namespace TMall.Infrastructure.SearchModel
{
    /// <summary>
    /// 用户自动收集搜索条件的Model
    /// </summary>
    public class SearchCondition
    {
        public SearchCondition()
        {
            Items = new List<ConditionItem>();
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public ICollection<ConditionItem> Items { get; internal set; }     

    }
}