using System;
namespace WD.Logging.Abstractions
{
    public interface ILogger<T> : ILogger
    {

    }

    public interface ILogger
    {
        #region Fatal
        /// <summary>
        /// Log fatal error
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Optional error message</param>
        /// <param name="args">Message arguments</param>
        void Fatal(Exception ex, string message = null, params object[] args);
        #endregion

        #region Error
        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="args">Message arguments</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Optional error message</param>
        /// <param name="args">Message arguments</param>
        void Error(Exception ex, string message = null, params object[] args);
        #endregion

        #region Warning
        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="args">Message arguments</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Log warning
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Optional error message</param>
        /// <param name="args">Message arguments</param>
        void Warn(Exception ex, string message = null, params object[] args);
        #endregion

        #region Info
        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="args">Message arguments</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Optional error message</param>
        /// <param name="args">Message arguments</param>
        void Info(Exception ex, string message = null, params object[] args);
        #endregion

        #region Debug
        /// <summary>
        /// Log debug information
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="args">Message arguments</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Log debug information
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Optional error message</param>
        /// <param name="args">Message arguments</param>
        void Debug(Exception ex, string message = null, params object[] args);
        #endregion

        #region Trace
        /// <summary>
        /// Log trace ionformation
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="args">Message arguments</param>
        void Trace(string message, params object[] args);

        #endregion
    }
}
