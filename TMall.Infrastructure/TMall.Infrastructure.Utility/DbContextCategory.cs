using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Infrastructure.Utility
{
    /// <summary>
    /// DbContext 分类名称
    /// </summary>
    public enum DbContextCategory
    {
        /// <summary>
        /// 主数据库
        /// </summary>
        MallDBContext = 1,

        /// <summary>
        /// 后台管理数据库
        /// </summary>
        ManageDBContext = 2
    }
}
