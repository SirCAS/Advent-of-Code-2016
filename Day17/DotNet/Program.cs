using System;

namespace Day17
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 17 |");
            Console.WriteLine("+-------------------------+");

            var input = "mmsxrhfx";

            var maze = new Maze();
            var solutions = maze.GetSolutions(input);
            Console.WriteLine($"Shortest route is {solutions.Shortest} (len: {solutions.Shortest.Length})");
            Console.WriteLine($"Longest route is {solutions.Longest} (len: {solutions.Longest.Length})");
            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}