using System;
namespace WD.Logging.Abstractions
{
    public interface ILoggerConfiguration
    {
        void ApplyConfiguration(ILoggerOptions);
    }
}
