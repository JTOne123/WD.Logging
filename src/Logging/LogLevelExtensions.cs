namespace WD.Logging
{
    public static class LogLevelExtensions
    {
        /// <summary>
        /// Convert NLog log level to library log level
        /// </summary>
        /// <returns>Library log level</returns>
        /// <param name="level">NLog log level</param>
        public static WD.Logging.Abstractions.LogLevel ToLib(this NLog.LogLevel level)
        {
            if (level == NLog.LogLevel.Debug)
            {
                return Abstractions.LogLevel.Debug;
            }

            if (level == NLog.LogLevel.Error)
            {
                return Abstractions.LogLevel.Error;
            }

            if (level == NLog.LogLevel.Fatal)
            {
                return Abstractions.LogLevel.Fatal;
            }

            if (level == NLog.LogLevel.Info)
            {
                return Abstractions.LogLevel.Information;
            }

            if (level == NLog.LogLevel.Trace)
            {
                return Abstractions.LogLevel.Trace;
            }

            if (level == NLog.LogLevel.Warn)
            {
                return Abstractions.LogLevel.Warning;
            }

            return Abstractions.LogLevel.Off;
        }

        /// <summary>
        /// Convert library log level to NLog log level
        /// </summary>
        /// <returns>NLog log level</returns>
        /// <param name="level">Library log level</param>
        public static NLog.LogLevel ToNLog(this Abstractions.LogLevel level)
        {
            switch (level)
            {
                case Abstractions.LogLevel.Debug:
                    return NLog.LogLevel.Debug;

                case Abstractions.LogLevel.Error:
                    return NLog.LogLevel.Error;

                case Abstractions.LogLevel.Fatal:
                    return NLog.LogLevel.Fatal;

                case Abstractions.LogLevel.Information:
                    return NLog.LogLevel.Info;

                case Abstractions.LogLevel.Trace:
                    return NLog.LogLevel.Trace;

                case Abstractions.LogLevel.Warning:
                    return NLog.LogLevel.Warn;

                default:
                    return NLog.LogLevel.Off;
            }
        }
    }
}
