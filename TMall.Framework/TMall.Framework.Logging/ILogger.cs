using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMall.Framework.Logging
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 是否可以记录日志
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        void Log(LogLevel logLevel, Exception exception, string format, params object[] args);
    }
}
