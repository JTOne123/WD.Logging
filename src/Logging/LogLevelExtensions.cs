using WD.Logging.Abstractions;

namespace WD.Logging
{
    /// <summary>
    ///     Extensions for log level
    /// </summary>
    public static class LogLevelExtensions
    {
        /// <summary>
        ///     Convert NLog log level to library log level
        /// </summary>
        /// <returns>Library log level</returns>
        /// <param name="level">NLog log level</param>
        public static LogLevel ToLib(this NLog.LogLevel level)
        {
            if (level == NLog.LogLevel.Debug)
            {
                return LogLevel.Debug;
            }

            if (level == NLog.LogLevel.Error)
            {
                return LogLevel.Error;
            }

            if (level == NLog.LogLevel.Fatal)
            {
                return LogLevel.Fatal;
            }

            if (level == NLog.LogLevel.Info)
            {
                return LogLevel.Information;
            }

            if (level == NLog.LogLevel.Trace)
            {
                return LogLevel.Trace;
            }

            if (level == NLog.LogLevel.Warn)
            {
                return LogLevel.Warning;
            }

            return LogLevel.Off;
        }

        /// <summary>
        ///     Convert library log level to NLog log level
        /// </summary>
        /// <returns>NLog log level</returns>
        /// <param name="level">Library log level</param>
        public static NLog.LogLevel ToNLog(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;

                case LogLevel.Error:
                    return NLog.LogLevel.Error;

                case LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;

                case LogLevel.Information:
                    return NLog.LogLevel.Info;

                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;

                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;

                default:
                    return NLog.LogLevel.Off;
            }
        }
    }
}