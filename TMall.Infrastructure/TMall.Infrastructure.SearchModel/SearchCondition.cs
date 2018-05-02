using System.Collections.Generic;

namespace TMall.Infrastructure.SearchModel
{
    /// <summary>
    /// �û��Զ��ռ�����������Model
    /// </summary>
    public class SearchCondition
    {
        public SearchCondition()
        {
            Items = new List<ConditionItem>();
        }
        /// <summary>
        /// ��ѯ����
        /// </summary>
        public ICollection<ConditionItem> Items { get; internal set; }     

    }
}