using System;
namespace WD.Logging.Abstractions
{
    public interface ILoggerConfiguration
    {
        void ApplyConfiguration(LoggerOptions options);
        void ApplyConfiguration(Func<LoggerOptions, LoggerOptions> options);

        ReadonlyLoggerOptions CurrentOptions { get; }
    }
}
