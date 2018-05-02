using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace TMall.Framework.Logging
{
    public class DefaultLog : ILogger
    {
        private static readonly ILog _log = LogManager.GetLogger("TMall.Logger");

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Fatal:
                    return _log.IsFatalEnabled;
            }
            return false;
        }

        public void Log(LogLevel logLevel, Exception exception, string format, params object[] args)
        {
            string message = args == null ? format : string.Format(format, args);

            switch (logLevel)
            {
                case LogLevel.Debug:
                    _log.Debug(message, exception);
                    break;
                case LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    _log.Fatal(message, exception);
                    break;
                case LogLevel.Information:
                    _log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message, exception);
                    break;
            }
        }
    }
}
