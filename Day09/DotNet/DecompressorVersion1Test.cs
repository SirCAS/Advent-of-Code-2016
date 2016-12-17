
using NUnit.Framework;

namespace Day09
{
    [TestFixture]
    public class DecompressorVersion1Tests
    {
        [Test]
        public void NoMarkers_Test()
        {
            // 1. Arrange
            const string input = "ADVENT";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("ADVENT", decompressor.Output);
            Assert.AreEqual(6, decompressor.Length);
        }

        [Test]
        public void SampleInput1_Test()
        {
            // 1. Arrange
            const string input = "A(1x5)BC";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("ABBBBBC", decompressor.Output);
            Assert.AreEqual(7, decompressor.Length);
        }

        [Test]
        public void SampleInput2_Test()
        {
            // 1. Arrange
            const string input = "(3x3)XYZ";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("XYZXYZXYZ", decompressor.Output);
            Assert.AreEqual(9, decompressor.Length);
        }

        [Test]
        public void SampleInput3_Test()
        {
            // 1. Arrange
            const string input = "A(2x2)BCD(2x2)EFG";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("ABCBCDEFEFG", decompressor.Output);
            Assert.AreEqual(11, decompressor.Length);
        }

        [Test]
        public void SampleInput4_Test()
        {
            // 1. Arrange
            const string input = "(6x1)(1x3)A";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("(1x3)A", decompressor.Output);
            Assert.AreEqual(6, decompressor.Length);
        }

        [Test]
        public void SampleInput5_Test()
        {
            // 1. Arrange
            const string input = "X(8x2)(3x3)ABCY";

            // 2. Act
            var decompressor = new DecompressorVersion1(input);

            // 3. Assert
            Assert.AreEqual("X(3x3)ABC(3x3)ABCY", decompressor.Output);
            Assert.AreEqual(18, decompressor.Length);
        }
    }
}