using System;
using FluentAssertions;
using NUnit.Framework;
using WD.Logging;
using WD.Logging.Abstractions;

namespace Logging.Tests
{
    [TestFixture]
    public class NLogLoggerConfigurationTests
    {
        [Test]
        public void Constructor_CurrentOptionsWithDefaultValues()
        {
            // Act
            var sut = new NLogLoggerConfiguration().CurrentOptions;

            // Assert
            sut.FileName.Should().BeNull();
            sut.IsCompressed.Should().BeFalse();
            sut.Level.Should().Be(WD.Logging.Abstractions.LogLevel.Off);
			sut.ArchiveCount.Should().Be(5);
            sut.SizePerFile.Size.Should().Be(0);
            sut.SizePerFile.SizeInBytes.Should().Be(0);
            sut.SizePerFile.SizeType.Should().Be(WD.Logging.Abstractions.SizeType.Byte);
            sut.IsArchiveOnStart.Should().BeFalse();
            sut.Filter.Should().Be("*");
        }

        [Test]
        public void ApplyConfiguration_WithNoOptions_Throws()
        {
            // Arrange
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration((LoggerOptions)null));

            // Act / Assert
            sutAction.Should().ThrowExactly<ArgumentNullException>()
                     .Which.ParamName.Should().Be("options");
        }

        [Test]
        public void ApplyConfiguration_WithNoOptionFunction_Throws()
        {
            // Arrange
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration((Func<LoggerOptions, LoggerOptions>)null));

            // Act / Assert
            sutAction.Should().ThrowExactly<ArgumentNullException>()
                     .Which.ParamName.Should().Be("options");
        }

        [Test]
        public void ApplyConfiguration_WithEmptyOptions_Throws()
        {
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration(new LoggerOptions()));

            // Act / Assert
            sutAction.Should().ThrowExactly<ArgumentException>()
                     .Which.ParamName.Should().Be(nameof(LoggerOptions.SizePerFile));
        }

        [Test]
        public void ApplyConfiguration_WithoutFilename_Throws()
        {
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration(new LoggerOptions().WithMaxSize(new FileSize{Size = 10000})));

            // Act / Assert
            sutAction.Should().ThrowExactly<ArgumentException>()
                     .Which.ParamName.Should().Be(nameof(LoggerOptions.FileName));
        }
    }
}
