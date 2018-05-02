using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Logging
{
    public enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 10,

        /// <summary>
        /// 输出信息
        /// </summary>
        Information = 20,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 30,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 40,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 50
    }
}
