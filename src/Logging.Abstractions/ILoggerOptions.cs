using System;
namespace WD.Logging.Abstractions
{
    /// <summary>
    /// Options to comnfigure the logger
    /// </summary>
    public interface ILoggerOptions
    {
        /// <summary>
        /// File name for logging
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// Are the archived log files archived
        /// </summary>
        bool IsCompressed { get; set; }
        /// <summary>
        /// Current log level
        /// </summary>
        LogLevel Level { get; set; }
        /// <summary>
        /// Max count for archive files
        /// </summary>
        int ArchiveCount { get; set; }
        /// <summary>
        /// Max file size before archive
        /// </summary>
        FileSize SizePerFile { get; set; }
        /// <summary>
        /// Create new log file on start and archive privious (also if the file size lower than max file size)
        /// </summary>
        /// <value><c>true</c> if is archive on start; otherwise, <c>false</c>.</value>
        bool IsArchiveOnStart { get; set; }

        /// <summary>
        /// Set log level with fluent API
        /// </summary>
        /// <param name="level"><see cref="Level"/></param>
        ILoggerOptions WithLevel(LogLevel level);
        /// <summary>
        /// Set log file name with fluent API
        /// </summary>
        /// <param name="fileName"><see cref="FileName"/></param>
        ILoggerOptions WithFile(string fileName);
        /// <summary>
        /// Set compression with fluent API
        /// </summary>
        /// <param name="compress"><see cref="IsCompressed"/></param>
        ILoggerOptions Compress(bool compress = true);
        /// <summary>
        /// Set max archive count with fluent API
        /// </summary>
        /// <param name="count"><see cref="ArchiveCount"/></param>
        ILoggerOptions WithArchiveCount(int count = 5);
        /// <summary>
        /// Set max log file size before archive with fluent API
        /// </summary>
        /// <param name="fileSize"><see cref="SizePerFile"/></param>
        ILoggerOptions WithMaxSize(FileSize fileSize);
        /// <summary>
        /// Set, if new file should be created on start with fluent API
        /// </summary>
        /// <param name="archiveOnStart"><see cref="IsArchiveOnStart"/></param>
        ILoggerOptions ArchiveOnStart(bool archiveOnStart = true);
    }

    /// <summary>
    /// Type safe file size converter
    /// </summary>
    public struct FileSize
    {
        /// <summary>
        /// Typed size
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Size type for conversion
        /// </summary>
        public SizeType SizeType { get; set; }

        /// <summary>
        /// Calculated size in bytes
        /// </summary>
        public int SizeInBytes {
            get {
                switch(SizeType)
                {
                    case SizeType.KiloByte:
                        return Size * 1000;

                    case SizeType.KibiByte:
                        return Size * 1024;

                    case SizeType.MegaByte:
                        return Size * 1000 * 1000;

                    case SizeType.MibiByte:
                        return Size * 1024 * 1024;

                    default:
                        return Size;
                }
            }
        }
    }

    /// <summary>
    /// Size types
    /// </summary>
    public enum SizeType
    {
        /// <summary>
        /// Megabyte (1000 factors)
        /// </summary>
        MB = 1,
        /// <summary>
        /// Megabyte (1000 factors)
        /// </summary>
        MegaByte = 1,
        /// <summary>
        /// Mibibyte (1024 factors)
        /// </summary>
        MiBi = 2,
        /// <summary>
        /// Mibibyte (1024 factors)
        /// </summary>
        MibiByte = 2,
        /// <summary>
        /// Kilobyte (1000 bytes)
        /// </summary>
        KB = 3,
        /// <summary>
        /// Kilobyte (1000 bytes)
        /// </summary>
        KiloByte = 3,
        /// <summary>
        /// Kibibyte (1024 bytes)
        /// </summary>
        KiBi = 4,
        /// <summary>
        /// Kibibyte (1024 bytes)
        /// </summary>
        KibiByte = 4,
        /// <summary>
        /// Bytes
        /// </summary>
        Byte = 5,
    }
}
