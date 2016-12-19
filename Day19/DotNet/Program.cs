using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day19
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 19 |");
            Console.WriteLine("+-------------------------+");

            //const int elfeCount = 5;
            const int elfeCount   = 3018458;
            Part1(elfeCount);
            Part2(elfeCount);

            Console.WriteLine($"  -Glædelig jul!");
        }

        private static void Part1(int elfeCount)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            var queue = new Queue<int>(Enumerable.Range(1, elfeCount).Select(x => x));
            while(queue.Count > 1)
            {
                var first = queue.Dequeue();
                var next = queue.Dequeue();
                queue.Enqueue(first);
            }

            sw.Stop();

            var winner = queue.Dequeue();
            Console.WriteLine($"Part1 winner is {winner} using {sw.Elapsed.TotalSeconds} seconds");
        }

        private static void Part2(int elfeCount)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            var list = Enumerable.Range(1, elfeCount).Select(x => x).ToList();
            var count = list.Count;
            while(count > 1)
            {
                var first = list[0];
                var nextIndex = count / 2;
                var next = list[nextIndex];
                list.RemoveAt(0);
                list.RemoveAt(nextIndex - 1);
                list.Add(first);
                count = list.Count;
            }

            sw.Stop();

            var winner = list[0];
            Console.WriteLine($"Part2 winner is {winner} using {sw.Elapsed.TotalSeconds} seconds");
        }
    }
}