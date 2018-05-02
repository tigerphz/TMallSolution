using System;

namespace TMall.Framework.Logging
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, Exception exception, string format, params object[] args)
        {
            FilteredLog(logger, LogLevel.Debug, exception, format, args);
        }
        public static void Information(this ILogger logger, Exception exception, string format, params object[] args)
        {
            FilteredLog(logger, LogLevel.Information, exception, format, args);
        }
        public static void Warning(this ILogger logger, Exception exception, string format, params object[] args)
        {
            FilteredLog(logger, LogLevel.Warning, exception, format, args);
        }
        public static void Error(this ILogger logger, Exception exception, string format, params object[] args)
        {
            FilteredLog(logger, LogLevel.Error, exception, format, args);
        }
        public static void Fatal(this ILogger logger, Exception exception, string format, params object[] args)
        {
            FilteredLog(logger, LogLevel.Fatal, exception, format, args);
        }

        private static void FilteredLog(ILogger logger, LogLevel logLevel, Exception exception, string format, params object[] args)
        {
            //不记录线程终止异常
            if ((exception != null) && (exception is System.Threading.ThreadAbortException))
                return;

            if (logger.IsEnabled(logLevel))
            {
                logger.Log(logLevel, exception, format, args);
            }
        }
    }
}
