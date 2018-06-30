namespace WD.Logging.Abstractions
{
    /// <summary>
    ///     Options to comnfigure the logger
    /// </summary>
    public class LoggerOptions : ReadonlyLoggerOptions
    {
        /// <summary>
        ///     Initialize from readonly options
        /// </summary>
        /// <param name="readOnlyOptions">Read only options</param>
        public static LoggerOptions CopyFromReadonly(ReadonlyLoggerOptions readOnlyOptions)
        {
            return new LoggerOptions
            {
                FileName = readOnlyOptions.FileName,
                IsCompressed = readOnlyOptions.IsCompressed,
                Level = readOnlyOptions.Level,
                ArchiveCount = readOnlyOptions.ArchiveCount,
                SizePerFile = readOnlyOptions.SizePerFile,
                IsArchiveOnStart = readOnlyOptions.IsArchiveOnStart,
                Filter = readOnlyOptions.Filter,
                LogMessageLayout = readOnlyOptions.LogMessageLayout,
                LogToDebugStream = readOnlyOptions.LogToDebugStream
            };
        }

        /// <summary>
        ///     Set log level with fluent API
        /// </summary>
        /// <param name="level">
        ///     <see cref="ReadonlyLoggerOptions.Level" />
        /// </param>
        public LoggerOptions WithLevel(LogLevel level)
        {
            Level = level;
            return this;
        }

        /// <summary>
        ///     Set log file name with fluent API
        /// </summary>
        /// <param name="fileName">
        ///     <see cref="ReadonlyLoggerOptions.FileName" />
        /// </param>
        public LoggerOptions WithFile(string fileName)
        {
            FileName = fileName;
            return this;
        }

        /// <summary>
        ///     Set compression with fluent API
        /// </summary>
        /// <param name="compress">
        ///     <see cref="ReadonlyLoggerOptions.IsCompressed" />
        /// </param>
        public LoggerOptions Compress(bool compress = true)
        {
            IsCompressed = compress;
            return this;
        }

        /// <summary>
        ///     Set max archive count with fluent API
        /// </summary>
        /// <param name="count">
        ///     <see cref="ReadonlyLoggerOptions.ArchiveCount" />
        /// </param>
        public LoggerOptions WithArchiveCount(int count = 5)
        {
            ArchiveCount = count;
            return this;
        }

        /// <summary>
        ///     Set max log file size before archive with fluent API
        /// </summary>
        /// <param name="fileSize">
        ///     <see cref="ReadonlyLoggerOptions.SizePerFile" />
        /// </param>
        public LoggerOptions WithMaxSize(FileSize fileSize)
        {
            SizePerFile = fileSize;
            return this;
        }

        /// <summary>
        ///     Set, if new file should be created on start with fluent API
        /// </summary>
        /// <param name="archiveOnStart">
        ///     <see cref="ReadonlyLoggerOptions.IsArchiveOnStart" />
        /// </param>
        public LoggerOptions ArchiveOnStart(bool archiveOnStart = true)
        {
            IsArchiveOnStart = archiveOnStart;
            return this;
        }

        /// <summary>
        ///     Set the logger filter
        /// </summary>
        /// <param name="filter">Filter definition (NULL -> no filter)</param>
        public LoggerOptions WithFilter(string filter)
        {
            Filter = filter;
            return this;
        }

        /// <summary>
        ///     Reset the logger filter to default
        /// </summary>
        public LoggerOptions WithoutFilter()
        {
            return WithFilter(null);
        }

        /// <summary>
        ///     Set log message layout
        /// </summary>
        /// <param name="messageLayout">Log message layout</param>
        public LoggerOptions WithLogMessageLayout(string messageLayout)
        {
            LogMessageLayout = string.IsNullOrWhiteSpace(messageLayout) ? null : messageLayout;
            return this;
        }

        /// <summary>
        ///     Set the default log message layout
        /// </summary>
        /// <returns></returns>
        public LoggerOptions WithDefaultLogMessagelayout()
        {
            return WithLogMessageLayout(null);
        }

        /// <summary>
        ///     Add the debug output
        /// </summary>
        /// <param name="debugOutput">Deafault: true</param>
        public LoggerOptions AddDebugTarget(bool debugOutput = true)
        {
            LogToDebugStream = debugOutput;
            return this;
        }

        /// <summary>
        ///     Remove debug traget, if any
        /// </summary>
        public LoggerOptions RemoveDebugTarget()
        {
            return AddDebugTarget(false);
        }
    }
}