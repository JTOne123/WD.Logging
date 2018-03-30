using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using WD.Logging;

namespace Logging.Tests
{
    [TestFixture]
    public class LogLevelExtensionsTests
    {
        [TestCaseSource(typeof(MappingSource), "Mappings")]
        public void Convert_Loglevel_InNLogLogLevel(NLog.LogLevel nlevel, WD.Logging.Abstractions.LogLevel wlevel)
        {
            // Act
            var result = wlevel.ToNLog();

            // Assert
            result.Should().Be(nlevel);
        }

        [TestCaseSource(typeof(MappingSource), "Mappings")]
        public void Convert_NLogLoglevel_InLogLevel(NLog.LogLevel nlevel, WD.Logging.Abstractions.LogLevel wlevel)
        {
            // Act
            var result = nlevel.ToLib();

            // Assert
            result.Should().Be(wlevel);
        }
    }

    public class MappingSource
    {
        public static IEnumerable Mappings
        {
            get
            {
                yield return new TestCaseData(NLog.LogLevel.Off, WD.Logging.Abstractions.LogLevel.Off);
                yield return new TestCaseData(NLog.LogLevel.Fatal, WD.Logging.Abstractions.LogLevel.Fatal);
                yield return new TestCaseData(NLog.LogLevel.Error, WD.Logging.Abstractions.LogLevel.Error);
                yield return new TestCaseData(NLog.LogLevel.Warn, WD.Logging.Abstractions.LogLevel.Warning);
                yield return new TestCaseData(NLog.LogLevel.Info, WD.Logging.Abstractions.LogLevel.Information);
                yield return new TestCaseData(NLog.LogLevel.Debug, WD.Logging.Abstractions.LogLevel.Debug);
                yield return new TestCaseData(NLog.LogLevel.Trace, WD.Logging.Abstractions.LogLevel.Trace);
            }
        }
    }
}
