﻿using System;
using WD.Logging.Abstractions;

namespace WD.Logging
{
    public class NLogLoggerConfiguration : ILoggerConfiguration
    {
        /// <summary>
        ///     Default log layout with 10 levels of exceptions
        /// </summary>
        public const string NLOG_LAYOUT =
            "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=toString,Data:maxInnerExceptionLevel=10}";

        /// <inheritdoc />
        public virtual ReadonlyLoggerOptions CurrentOptions { get; private set; } = new ReadonlyLoggerOptions();

        /// <inheritdoc />
        public virtual void ApplyConfiguration(LoggerOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options), "Null options are not allowed");
            }

            CurrentOptions = options;

            // Validate options
            ValidateOptions(CurrentOptions);

            // Reconfigure NLog
            var fileTarget = new NLog.Targets.FileTarget("FileLog")
            {
                FileName = options.FileName,
                ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.DateAndSequence,
                ArchiveOldFileOnStartup = options.IsArchiveOnStart,
                Layout = NLOG_LAYOUT,
                EnableArchiveFileCompression = options.IsCompressed,
                ArchiveAboveSize = options.SizePerFile.SizeInBytes,
                MaxArchiveFiles = options.ArchiveCount
            };

            var configuration = new NLog.Config.LoggingConfiguration();
            configuration.AddTarget(fileTarget);

            // Configure rule
            var filter = string.IsNullOrWhiteSpace(options.Filter) ? "*" : options.Filter;
            var fileRule = new NLog.Config.LoggingRule(filter, options.Level.ToNLog(), fileTarget);
            configuration.LoggingRules.Add(fileRule);

            // Set configuration
            NLog.LogManager.Configuration = configuration;

        }

        /// <inheritdoc />
        public virtual void ApplyConfiguration(Func<LoggerOptions, LoggerOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Null options are not allowed");
            }

            var configOptions = options(new LoggerOptions());
            ApplyConfiguration(configOptions);
        }

        /// <inheritdoc />
        public void ChangeConfiguration(Func<LoggerOptions, LoggerOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Null options are not allowed");
            }
            var configOptions = options(new LoggerOptions(CurrentOptions));
            ApplyConfiguration(configOptions);
        }

        /// <summary>
        /// Validate the options fo minimum data
        /// </summary>
        /// <param name="options">Logger options</param>
        protected virtual void ValidateOptions(ReadonlyLoggerOptions options)
        {
            if (options.SizePerFile.SizeInBytes <= 1000)
            {
                throw new ArgumentException("The file size have to be positive and at least 1000  bytes", nameof(ReadonlyLoggerOptions.SizePerFile));
            }

            if (string.IsNullOrWhiteSpace(options.FileName))
            {
                throw new ArgumentException("The file name should be set", nameof(ReadonlyLoggerOptions.FileName));
            }
        }
    }
}
