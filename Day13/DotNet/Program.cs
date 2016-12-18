using System;

namespace Day13
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 13 |");
            Console.WriteLine("+-------------------------+");

            const int favoriteNumber = 1358;
            var navigator = new Navigator(favoriteNumber);

            // Part 1
            var startPos = new Pos {X = 1, Y = 1};
            var targetPos = new Pos {X = 31, Y = 39};
            var steps = navigator.FindLeastSteps(startPos, targetPos);
            Console.WriteLine($"The least steps possible is {steps}");

            // Part 2
            const int targetSteps = 50;
            var distinctLocations = navigator.FindDistinctLocations(startPos, targetSteps);
            Console.WriteLine($"There is distinct locations {distinctLocations}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}