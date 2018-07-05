using System;
using NLog;
using WD.Logging.Abstractions;
using ILogger = WD.Logging.Abstractions.ILogger;

namespace WD.Logging
{
    /// <summary>
    ///     Generic logging adapter for NLog
    /// </summary>
    /// <typeparam name="T">Type for logged class</typeparam>
    public class NLogLoggerAdapter<T> : NLogLoggerAdapter, ILogger<T>
    {
        /// <inheritdoc />
        public NLogLoggerAdapter() : base(typeof(T))
        {
        }
    }

    /// <summary>
    ///     Logging adapter for NLog
    /// </summary>
    public class NLogLoggerAdapter : ILogger
    {
        private readonly NLog.ILogger _logger;

        /// <inheritdoc />
        public NLogLoggerAdapter(Type loggerType)
        {
            _logger = LogManager.GetLogger(loggerType.FullName);
        }

        #region Fatal

        /// <inheritdoc />
        public void Fatal(Exception ex, string message = null, params object[] args)
        {
            _logger.Fatal(ex, message, args);
        }

        #endregion

        #region Trace

        /// <inheritdoc />
        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        #endregion

        #region Debug

        /// <inheritdoc />
        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        /// <inheritdoc />
        public void Debug(Exception ex, string message = null, params object[] args)
        {
            _logger.Debug(ex, message, args);
        }

        #endregion

        #region Errors

        /// <inheritdoc />
        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        /// <inheritdoc />
        public void Error(Exception ex, string message = null, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        #endregion

        #region Info

        /// <inheritdoc />
        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        /// <inheritdoc />
        public void Info(Exception ex, string message = null, params object[] args)
        {
            _logger.Info(ex, message, args);
        }

        #endregion

        #region Warning

        /// <inheritdoc />
        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        /// <inheritdoc />
        public void Warn(Exception ex, string message = null, params object[] args)
        {
            _logger.Warn(ex, message, args);
        }

        #endregion
    }
}