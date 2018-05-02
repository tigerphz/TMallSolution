using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TMall.Infrastructure.Web
{
    /// <summary>
    /// DrowDownList下拉框追加item类型
    /// </summary>
    public enum AppendItemType
    {
        None,

        /// <summary>
        /// --全部--
        /// </summary>
        [Description("--全部--")]
        All,

        /// <summary>
        /// --请选择--
        /// </summary>
        [Description("--请选择--")]
        Select
    }
}
