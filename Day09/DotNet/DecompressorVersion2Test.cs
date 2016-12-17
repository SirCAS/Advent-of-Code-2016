
using NUnit.Framework;

namespace Day09
{
    [TestFixture]
    public class DecompressorVersion2Tests
    {
        [Test]
        public void NoMarkers_Test()
        {
            // 1. Arrange
            const string input = "ADVENT";

            // 2. Act
            var decompressor = new DecompressorVersion2(input);

            // 3. Assert
            Assert.AreEqual(6, decompressor.Length);
        }

        [Test]
        public void SampleInput1_Test()
        {
            // 1. Arrange
            const string input = "(3x3)XYZ";

            // 2. Act
            var decompressor = new DecompressorVersion2(input);

            // 3. Assert
            Assert.AreEqual(9, decompressor.Length);
        }

        [Test]
        public void SampleInput2_Test()
        {
            // 1. Arrange
            const string input = "X(8x2)(3x3)ABCY";

            // 2. Act
            var decompressor = new DecompressorVersion2(input);

            // 3. Assert
            Assert.AreEqual(20, decompressor.Length);
        }

        [Test]
        public void SampleInput3_Test()
        {
            // 1. Arrange
            const string input = "(27x12)(20x12)(13x14)(7x10)(1x12)A";

            // 2. Act
            var decompressor = new DecompressorVersion2(input);

            // 3. Assert
            Assert.AreEqual(241920, decompressor.Length);
        }

        [Test]
        public void SampleInput4_Test()
        {
            // 1. Arrange
            const string input = "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN";

            // 2. Act
            var decompressor = new DecompressorVersion2(input);

            // 3. Assert
            Assert.AreEqual(445, decompressor.Length);
        }
    }
}