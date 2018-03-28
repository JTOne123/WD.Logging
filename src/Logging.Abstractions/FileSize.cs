namespace WD.Logging.Abstractions
{
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
        public int SizeInBytes
        {
            get
            {
                switch (SizeType)
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
}
