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
        private const string _FATAL= "Fatal";
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

        [Test]
        public void LevelOff_NoLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Off);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            sut.Trace(_TRACE);
            sut.Debug(_DEBUG);
            sut.Info(_INFO);
            sut.Warn(_WARN);
            sut.Error(_ERROR);
            sut.Fatal(_FATAL_EX, _FATAL);

            // Assert
            target.Counter.Should().Be(0);
        }

        [Test]
        public void LevelFatal_OnlyTraceLog()
        {
            // Arrange
            var target = new DebugTarget();
            target.Layout = _LAYOUT;
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Fatal);
            var sut = new WD.Logging.NLogLoggerAdapter<NLogAdapterTests>();

            // Act
            sut.Trace(_TRACE);
            sut.Debug(_DEBUG);
            sut.Info(_INFO);
            sut.Warn(_WARN);
            sut.Error(_ERROR);
            sut.Fatal(_FATAL_EX, _FATAL);

            // Assert
            target.Counter.Should().Be(1);
            target.LastMessage.Should().Be(string.Format(_MESSAGE_FORMAT,
                                                         typeof(NLogAdapterTests).FullName,
                                                         LogLevel.Fatal,
                                                         _FATAL,
                                                         _FATAL));
        }
    }
}
