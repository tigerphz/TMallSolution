using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TMall.DomainModule.Enums
{
    /// <summary>
    /// 通用状态
    /// </summary>
    public enum CommonStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        [Description("无效")]
        Invalid = -1,

        /// <summary>
        /// 有效
        /// </summary>
        [Description("有效")]
        Valid = 0
    }
}
