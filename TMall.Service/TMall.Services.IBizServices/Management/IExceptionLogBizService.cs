using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using TMall.Framework.Logging;

namespace TMall.Services.IBizServices.Management
{
    public interface IExceptionLogBizService
    {
        bool IsEnabled(LogLevel level);

        void DeleteLog(Guid sysno);

        ExceptionLogInfo GetLogById(Guid sysno);

        IList<ExceptionLogInfo> GetLogByIds(Guid[] sysnos);

        SearchPageInfo<ExceptionLogInfo> GetLogs(PageInfo pageInfo);

        SearchPageInfo<ExceptionLogInfo> GetLogs(SearchPageInfo<ExceptionLogInfo> search);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        void Log(LogLevel logLevel, Exception exception, string format, params object[] args);
    }
}
