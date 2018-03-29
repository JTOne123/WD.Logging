using System;
namespace WD.Logging.Abstractions
{
    /// <summary>
    /// Interface to configure the logger
    /// </summary>
    public interface ILoggerConfiguration
    {
        /// <summary>
        /// Apply configuration options to logger
        /// </summary>
        /// <param name="options">Options to apply</param>
        void ApplyConfiguration(LoggerOptions options);
        /// <summary>
        /// Apply configuration options to logger
        /// </summary>
        /// <param name="options">Options as lambda</param>
        void ApplyConfiguration(Func<LoggerOptions, LoggerOptions> options);
        /// <summary>
        /// Change current options <see cref="CurrentOptions"/>
        /// </summary>
        /// <param name="options">Changed options</param>
        void ChangeConfiguration(Func<LoggerOptions, LoggerOptions> options);

        /// <summary>
        /// Current options of the logger
        /// </summary>
        ReadonlyLoggerOptions CurrentOptions { get; }
    }
}
