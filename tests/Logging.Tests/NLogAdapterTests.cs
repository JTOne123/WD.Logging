using System;
using FluentAssertions;
using NLog;
using NLog.Targets;
using NUnit.Framework;

namespace Logging.Tests
{
    [TestFixture]
    public class NLogAdapterTests
    {
        private const string _LAYOUT = "${logger}|${level}|${message}|${exception}";
        private const string _MESSAGE_FORMAT = "{0}|{1}|{2}|{3}";
        private const string _FATAL = "Fatal";
        private const string _ERROR = "Error";
        private const string _WARN = "Warning";
        private const string _INFO = "Information";
        private const string _DEBUG = "Debug";
        private const string _TRACE = "Trace";
        private readonly Exception _FATAL_EX = new Exception(_FATAL);
        private readonly Exception _ERROR_EX = new Exception(_ERROR);
        private readonly Exception _WARN_EX = new Exception(_WARN);
        private readonly Exception _INFO_EX = new Exception(_INFO);
        private readonly Exception _DEBUG_EX = new Exception(_DEBUG);
        private readonly Exception _TRACE_EX = new Exception(_TRACE);

        private void ExecuteLogs(WD.Logging.Abstractions.ILogger sut)
        {
            sut.Fatal(_FATAL_EX, _FATAL);
            sut.Error(_ERROR);
            sut.Warn(_WARN);
            sut.Info(_INFO);
            sut.Debug(_DEBUG);
            sut.Trace(_TRACE);
        }

        private void ExecuteLogsWithException(WD.Logging.Abstractions.ILogger sut)
        {
            sut.Fatal(_FATAL_EX, _FATAL);
            sut.Error(_ERROR_EX, _ERROR);
            sut.Warn(_WARN_EX, _WARN);
            sut.Info(_INFO_EX, _INFO);
            sut.Debug(_DEBUG_EX, _DEBUG);
            sut.Trace(_TRACE);
        }

        [Test]
        public void LevelOff_NoLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Off);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(0);
        }

        [Test]
        public void LevelFatal_OnlyFatalLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Fatal);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(1);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Fatal,
                                                         _FATAL,
                                                         _FATAL));
        }

        [Test]
        public void LevelError_OnlyErrorAndFatalLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Error);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(2);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Error,
                                                         _ERROR,
                                                         string.Empty));
        }

        [Test]
        public void LevelError_WithException_OnlyErrorAndFatalLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Error);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogsWithException(sut);

            // Assert
            target.Counter.Should().Be(2);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Error,
                                                         _ERROR,
                                                         _ERROR));
        }

        [Test]
        public void LevelWarn_OnlyWarnErrorAndFatalLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Warn);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(3);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Warn,
                                                         _WARN,
                                                         string.Empty));
        }

        [Test]
        public void LevelWarn_WithException_OnlyWarnErrorAndFatalLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Warn);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogsWithException(sut);

            // Assert
            target.Counter.Should().Be(3);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Warn,
                                                         _WARN,
                                                         _WARN));
        }

        [Test]
        public void LevelInfo_LogWithoutDebugAndTrace()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Info);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(4);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Info,
                                                         _INFO,
                                                         string.Empty));
        }

        [Test]
        public void LevelInfo_WithException_WithoutDebugAndTrace()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Info);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogsWithException(sut);

            // Assert
            target.Counter.Should().Be(4);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Info,
                                                         _INFO,
                                                         _INFO));
        }

        [Test]
        public void LevelDebug_LogWithoutTrace()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(5);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Debug,
                                                         _DEBUG,
                                                         string.Empty));
        }

        [Test]
        public void LevelDebug_WithException_WithoutTrace()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogsWithException(sut);

            // Assert
            target.Counter.Should().Be(5);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Debug,
                                                         _DEBUG,
                                                         _DEBUG));
        }

        [Test]
        public void LevelTrace_LogAll()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            ExecuteLogs(sut);

            // Assert
            target.Counter.Should().Be(6);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Trace,
                                                         _TRACE,
                                                         string.Empty));
        }
    }
}
