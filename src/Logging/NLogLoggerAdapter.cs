using System;
using WD.Logging.Abstractions;

namespace WD.Logging
{
    public class NLogLoggerAdapter<T> : NLogLoggerAdapter, ILogger<T>
    {
        public NLogLoggerAdapter() : base(typeof(T))
        {

        }
    }

    public class NLogLoggerAdapter : ILogger
    {
        private readonly NLog.ILogger _logger;
        public NLogLoggerAdapter(Type loggerType)
        {
            _logger = NLog.LogManager.GetLogger(loggerType.FullName);
        }

        #region Debug

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Debug(Exception ex, string message = null, params object[] args)
        {
            _logger.Debug(ex, message, args);
        }

        #endregion

        #region Errors

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception ex, string message = null, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        #endregion

        #region Fatal

        public void Fatal(Exception ex, string message = null, params object[] args)
        {
            _logger.Fatal(ex, message, args);
        }

        #endregion

        #region Info

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Info(Exception ex, string message = null, params object[] args)
        {
            _logger.Info(ex, message, args);
        }

        #endregion

        #region Trace

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        #endregion

        #region Warning

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Warn(Exception ex, string message = null, params object[] args)
        {
            _logger.Warn(ex, message, args);
        }

        #endregion
    }
}
