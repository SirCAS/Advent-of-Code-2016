using NUnit.Framework;

namespace Day17
{
    [TestFixture]
    public class MazeTests
    {
        [Test]
        public void GetShortestRoute_Sample1_Test()
        {
            // 1. Arrange
            var input = "ihgpwlah";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Shortest, Is.EqualTo("DDRRRD"));
        }

        [Test]
        public void GetShortestRoute_Sample2_Test()
        {
            // 1. Arrange
            var input = "kglvqrro";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Shortest, Is.EqualTo("DDUDRLRRUDRD"));
        }

        [Test]
        public void GetShortestRoute_Sample3_Test()
        {
            // 1. Arrange
            var input = "ulqzkmiv";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Shortest, Is.EqualTo("DRURDRUDDLLDLUURRDULRLDUUDDDRR"));
        }

        [Test]
        public void GetLongestRoute_Sample1_Test()
        {
            // 1. Arrange
            var input = "ihgpwlah";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Longest.Length, Is.EqualTo(370));
        }

        [Test]
        public void GetLongestRoute_Sample2_Test()
        {
            // 1. Arrange
            var input = "kglvqrro";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Longest.Length, Is.EqualTo(492));
        }

        [Test]
        public void GetLongestRoute_Sample3_Test()
        {
            // 1. Arrange
            var input = "ulqzkmiv";
            var maze = new Maze();

            // 2. Act
            var result = maze.GetSolutions(input);

            // 3. Assert
            Assert.That(result.Longest.Length, Is.EqualTo(830));
        }
    }
}