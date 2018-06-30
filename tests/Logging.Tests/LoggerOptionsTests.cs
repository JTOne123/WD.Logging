using System;
using FluentAssertions;
using NUnit.Framework;
using WD.Logging.Abstractions;

namespace Logging.Tests
{
    [TestFixture]
    public class LoggerOptionsTests
    {
        [TestCase(LogLevel.Off)]
        [TestCase(LogLevel.Fatal)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Information)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Trace)]
        public void WithLevel_SetLogLevelProperty(LogLevel level)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.WithLevel(level);

            // Assert
            sut.Level.Should().Be(level);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Compress_SetIsCompressedProperty(bool isCompressed)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.Compress(isCompressed);

            // Assert
            sut.IsCompressed.Should().Be(isCompressed);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void WithArchiveCount_SetArchiveCountProperty(int archiveCount)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.WithArchiveCount(archiveCount);

            // Assert
            sut.ArchiveCount.Should().Be(archiveCount);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ArchiveOnStart_SetArchiveOnStartProperty(bool archive)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.ArchiveOnStart(archive);

            // Assert
            sut.IsArchiveOnStart.Should().Be(archive);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  \t \r\n \r \n ")]
        public void WithLogMessageLayout_WithNullOrEmptyLayout_SetLogMessageLayoutProperty_ToDefault(
            string logMessageLayout)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.WithLogMessageLayout(logMessageLayout);

            // Assert
            sut.LogMessageLayout.Should().BeNull();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void AddDebugTarget_SetLogMessageLayoutProperty(bool debug)
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.AddDebugTarget(debug);

            // Assert
            sut.LogToDebugStream.Should().Be(debug);
        }

        [TestCase(true, true, true)]
        [TestCase(true, false, false)]
        [TestCase(false, true, false)]
        [TestCase(false, false, true)]
        public void CopyFromReadonly_CreateNewInstance_WithReadonlyValues(bool compressed, bool archiveOnStart,
            bool debug)
        {
            // Arrange
            var filter = Guid.NewGuid().ToString();
            var filename = Guid.NewGuid().ToString();
            var fileCount = 11;
            var fileSize = new FileSize {SizeType = SizeType.KibiByte, Size = 123456};
            var logLevel = LogLevel.Information;
            var logLayout = "${callsite}";
            ReadonlyLoggerOptions readOnly = new LoggerOptions()
                .WithLevel(logLevel)
                .WithFilter(filter)
                .WithFile(filename)
                .WithMaxSize(fileSize)
                .WithArchiveCount(fileCount)
                .Compress(compressed)
                .ArchiveOnStart(archiveOnStart)
                .WithLogMessageLayout(logLayout)
                .AddDebugTarget(debug);

            // Act
            var result = LoggerOptions.CopyFromReadonly(readOnly);

            // Assert
            result.ArchiveCount.Should().Be(fileCount);
            result.FileName.Should().Be(filename);
            result.Filter.Should().Be(filter);
            result.IsArchiveOnStart.Should().Be(archiveOnStart);
            result.IsCompressed.Should().Be(compressed);
            result.Level.Should().Be(logLevel);
            result.SizePerFile.SizeInBytes.Should().Be(fileSize.SizeInBytes);
            result.LogMessageLayout.Should().Be(logLayout);
            result.LogToDebugStream.Should().Be(debug);
        }

        [Test]
        public void RemoveDebugTarget_SetLogMessageLayoutProperty_ToFalse()
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.RemoveDebugTarget();

            // Assert
            sut.LogToDebugStream.Should().BeFalse();
        }

        [Test]
        public void WithDefaultLogMessagelayout_SetLogMessageLayoutProperty_ToDefault()
        {
            // Arrange
            var sut = new LoggerOptions();

            // Act
            sut.WithDefaultLogMessagelayout();

            // Assert
            sut.LogMessageLayout.Should().BeNull();
        }

        [Test]
        public void WithFile_SetFileNameProperty()
        {
            // Arrange
            var fileName = Guid.NewGuid().ToString();
            var sut = new LoggerOptions();

            // Act
            sut.WithFile(fileName);

            // Assert
            sut.FileName.Should().Be(fileName);
        }

        [Test]
        public void WithFilter_SetFilterProperty()
        {
            // Arrange
            var filter = Guid.NewGuid().ToString();
            var sut = new LoggerOptions();

            // Act
            sut.WithFilter(filter);

            // Assert
            sut.Filter.Should().Be(filter);
        }

        [Test]
        public void WithLogMessageLayout_SetLogMessageLayoutProperty()
        {
            // Arrange
            var logMessageLayout = Guid.NewGuid().ToString();
            var sut = new LoggerOptions();

            // Act
            sut.WithLogMessageLayout(logMessageLayout);

            // Assert
            sut.LogMessageLayout.Should().Be(logMessageLayout);
        }

        [Test]
        public void WithMaxSize_SetFileSizeProperty()
        {
            // Arrange
            var fileSize = new FileSize
            {
                Size = 123456,
                SizeType = SizeType.KibiByte
            };
            var sut = new LoggerOptions();

            // Act
            sut.WithMaxSize(fileSize);

            // Assert
            sut.SizePerFile.Size.Should().Be(123456);
            sut.SizePerFile.SizeType.Should().Be(SizeType.KiBi);
        }

        [Test]
        public void WithoutFilter_ResetFilterProperty()
        {
            // Arrange
            var filter = Guid.NewGuid().ToString();
            var sut = new LoggerOptions();

            // Act
            sut.WithFilter(filter);
            sut.WithoutFilter();

            // Assert
            sut.Filter.Should().BeNull();
        }
    }
}