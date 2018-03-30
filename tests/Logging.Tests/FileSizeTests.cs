using FluentAssertions;
using NUnit.Framework;
using WD.Logging.Abstractions;

namespace Logging.Tests
{
    [TestFixture]
    public class FileSizeTests
    {
        [TestCase(SizeType.Byte, 1000, 1000)]
        [TestCase(SizeType.KiloByte, 1000, 1000000)]
        [TestCase(SizeType.KB, 1000, 1000000)]
        [TestCase(SizeType.KibiByte, 1000, 1024000)]
        [TestCase(SizeType.KiBi, 1000, 1024000)]
        [TestCase(SizeType.MegaByte, 1000, 1000000000)]
        [TestCase(SizeType.MB, 1000, 1000000000)]
        [TestCase(SizeType.MibiByte, 1000, 1048576000)]
        [TestCase(SizeType.MiBi, 1000, 1048576000)]
        public void Validata_SizeInBytesConversion(SizeType type, int size, int expected)
        {
            // Arrange
            var sut = new FileSize
            {
                Size = size,
                SizeType = type
            };

            // Act
            var result = sut.SizeInBytes;

            // Assert
            result.Should().Be(expected);
        }
    }
}