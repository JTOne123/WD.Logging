using System;
namespace WD.Logging.Abstractions
{
    /// <summary>
    /// Log level
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Logging ist off
        /// </summary>
        Off = 0,
        /// <summary>
        /// Log only fatal errors
        /// </summary>
        Fatal = 1,
        /// <summary>
        /// Log only fatal errors and errors
        /// </summary>
        Error = 2,
        /// <summary>
        /// Log only fatal errors, errors and warnings
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Log all, but not debug and trace information
        /// </summary>
        Information = 4,
        /// <summary>
        /// Log all, but not trace information
        /// </summary>
        Debug = 5,
        /// <summary>
        /// Log all
        /// </summary>
        Trace = 6
    }
}
