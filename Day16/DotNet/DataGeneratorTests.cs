using NUnit.Framework;

namespace Day16
{
    [TestFixture]
    public class DataGeneratorTests
    {
        [Test]
        public void GenerateData_Sample1_Test()
        {
            // 1. Arrange
            const string input = "1";
            
            var generator = new DataGenerator();

            // 2. Act
            var result = generator.GenerateData(input);

            // 3. Assert
            Assert.AreEqual("100", result);
        }

        [Test]
        public void GenerateData_Sample2_Test()
        {
            // 1. Arrange
            const string input = "0";
            
            var generator = new DataGenerator();

            // 2. Act
            var result = generator.GenerateData(input);

            // 3. Assert
            Assert.AreEqual("001", result);
        }


        [Test]
        public void GenerateData_Sample3_Test()
        {
            // 1. Arrange
            const string input = "11111";
            
            var generator = new DataGenerator();

            // 2. Act
            var result = generator.GenerateData(input);

            // 3. Assert
            Assert.AreEqual("11111000000", result);
        }

        [Test]
        public void GenerateData_Sample4_Test()
        {
            // 1. Arrange
            const string input = "111100001010";
            
            var generator = new DataGenerator();

            // 2. Act
            var result = generator.GenerateData(input);

            // 3. Assert
            Assert.AreEqual("1111000010100101011110000", result);
        }

        [Test]
        public void CalculateChecksum_Sample1_Test()
        {
            // 1. Arrange
            const string input = "110010110100";

            var generator = new DataGenerator();

            // 2. Act
            var result = generator.CalculateChecksum(input);

            // 3. Assert
            Assert.AreEqual("100", result);
        }

        [Test]
        public void CalculateDiskChecksum_Sample1_Test()
        {
            // 1. Arrange
            const string input = "10000";
            const int length = 20;

            var generator = new DataGenerator();

            // 2. Act
            var result = generator.CalculateDiskChecksum(input, length);

            // 3. Assert
            Assert.AreEqual("01100", result);
        }
    }
}