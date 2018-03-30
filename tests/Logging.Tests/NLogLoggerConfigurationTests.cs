using System;
using System.Linq;
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
        public void ChangeConfiguration_WithNoOptionFunction_Throws()
        {
            // Arrange
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ChangeConfiguration((Func<LoggerOptions, LoggerOptions>)null));

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
            var exception = sutAction.Should().ThrowExactly<ArgumentException>();
            exception.Which.ParamName.Should().Be("options");
            exception.Which.Message.Should().Contain("file size");
        }

        [Test]
        public void ApplyConfiguration_WithoutFilename_Throws()
        {
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration(new LoggerOptions().WithMaxSize(new FileSize{Size = 10000})));

            // Act / Assert
            var exception = sutAction.Should().ThrowExactly<ArgumentException>();
            exception.Which.ParamName.Should().Be("options");
            exception.Which.Message.Should().Contain("file name");
        }

        [Test]
        public void ApplyConfiguration_WitFileSizeBelow1000_Throws()
        {
            var sut = new NLogLoggerConfiguration();
            var sutAction = new Action(() => sut.ApplyConfiguration(new LoggerOptions()
                .WithMaxSize(new FileSize{Size = 999})));

            // Act / Assert
            var exception = sutAction.Should().ThrowExactly<ArgumentException>();
            exception.Which.ParamName.Should().Be("options");
            exception.Which.Message.Should().Contain("file size").And.Contain("1000");
        }

        [Test]
        public void ApplayConfiguration_ValidateOnNlogConfiguration()
        {
            // Arrange
            var filter = Guid.NewGuid().ToString();
            var filename = Guid.NewGuid().ToString();
            var fileCount = 11;
            var fileSize = new FileSize{SizeType = SizeType.KibiByte, Size = 123456};
            var compressed = true;
            var archiveOnStart = true;
            var logLevel = LogLevel.Information;
            var sut = new NLogLoggerConfiguration();
            
            // Act
            sut.ApplyConfiguration(options => options
                .WithLevel(logLevel)
                .WithFilter(filter)
                .WithFile(filename)
                .WithMaxSize(fileSize)
                .WithArchiveCount(fileCount)
                .Compress(compressed)
                .ArchiveOnStart(archiveOnStart));

            // Assert
            var config = NLog.LogManager.Configuration;
            var target = config.AllTargets.Single() as NLog.Targets.FileTarget;
            target.ArchiveAboveSize.Should().Be(fileSize.SizeInBytes);
            (target.FileName as NLog.Layouts.SimpleLayout).Text.Should().Be(filename);
            target.ArchiveOldFileOnStartup.Should().Be(archiveOnStart);
            target.MaxArchiveFiles.Should().Be(fileCount);
            target.EnableArchiveFileCompression.Should().Be(compressed);
            var rule = config.LoggingRules.First();
            rule.Levels.Contains(logLevel.ToNLog());
        }

        [Test]
        public void ChangeConfiguration_ValidateOnNlogConfiguration()
        {
            // Arrange
            var filter = Guid.NewGuid().ToString();
            var filename = Guid.NewGuid().ToString();
            var fileCount = 11;
            var fileCountNew = 10;
            var fileSize = new FileSize{SizeType = SizeType.KibiByte, Size = 123456};
            var compressed = true;
            var archiveOnStart = true;
            var logLevel = LogLevel.Information;
            var sut = new NLogLoggerConfiguration();
            sut.ApplyConfiguration(options => options
                .WithLevel(logLevel)
                .WithFilter(filter)
                .WithFile(filename)
                .WithMaxSize(fileSize)
                .WithArchiveCount(fileCount)
                .Compress(compressed)
                .ArchiveOnStart(archiveOnStart));
            
            // Act
            sut.ChangeConfiguration(options => options.WithArchiveCount(fileCountNew));

            // Assert
            var config = NLog.LogManager.Configuration;
            var target = config.AllTargets.Single() as NLog.Targets.FileTarget;
            target.ArchiveAboveSize.Should().Be(fileSize.SizeInBytes);
            (target.FileName as NLog.Layouts.SimpleLayout).Text.Should().Be(filename);
            target.ArchiveOldFileOnStartup.Should().Be(archiveOnStart);
            target.MaxArchiveFiles.Should().Be(fileCountNew);
            target.EnableArchiveFileCompression.Should().Be(compressed);
            var rule = config.LoggingRules.First();
            rule.Levels.Contains(logLevel.ToNLog());
        }
    }
}

