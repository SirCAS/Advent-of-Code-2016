using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day1.ConsoleApplication
{
    public class Program
    {
        // The %-operator of C# is actually NOT a modulo for reasons?! 
        private static int mod(int k, int n) {  return ((k %= n) < 0) ? k+n : k;  }

        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 01 |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine();

            var input = File.ReadAllText("input.txt")
                .Split(new []{','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim());

            var position = new Tuple<int,int>(0, 0);
            var visits = new List<Tuple<int,int>> { position };
            var moves = new int[4];

            var currentDirection = 0; // North=0, 1=East, 2=South, 3=West 

            foreach(var instruction in input)
            {
                var turn = instruction[0];
                int length = int.Parse(new string(instruction.Skip(1).ToArray()));
                
                if(turn == 'R')
                    ++currentDirection;
                else
                    --currentDirection;

                currentDirection = mod(currentDirection, 4);

                for(int i=0; i<length; ++i)
                {
                    moves[currentDirection]++;
                    position = new Tuple<int, int>(moves[1] - moves[3], moves[0] - moves[2]);

                    if(visits.Any(x => x.Item1 == position.Item1 && x.Item2 == position.Item2))
                    {
                        int firstBlocks = Math.Abs(position.Item1) + Math.Abs(position.Item2);
                        Console.WriteLine($"Location is already visited @ ({position.Item1}, {position.Item2}) => {firstBlocks} blocks away");
                    } else {
                        visits.Add(position);
                    }
                }
            }
            int finalBlocks = Math.Abs(position.Item1) + Math.Abs(position.Item2);
            Console.WriteLine($"Final location is @ ({position.Item1},{position.Item2} blocks {finalBlocks} away");
            Console.WriteLine($"  -Gruss Gott");
        }
    }
}