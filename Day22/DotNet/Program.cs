using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 22 |");
            Console.WriteLine("+-------------------------+");

            var input = File
                .ReadAllLines("input.txt")
                .Skip(2)
                .Select(x => x.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new {
                    Name  = x[0],
                    Size  = int.Parse(x[1].Substring(0, x[1].Length - 1)),
                    Used  = int.Parse(x[2].Substring(0, x[2].Length - 1)),
                    Avail = int.Parse(x[3].Substring(0, x[3].Length - 1)),
                    //Use   = int.Parse(x[4].Substring(0, x[4].Length - 1)),
                    X     = int.Parse(x[0].Substring(16).Split('-')[0]),
                    Y     = int.Parse(x[0].Split('-')[2].Substring(1))
                })
                .ToList();

            var viablePairs = GetPermutations(input, 2)
                .Select(x => x.ToList())
                .Count(x =>
                    x[0].Used > 0 &&
                    x[0].Used <= x[1].Avail
                );

            Console.WriteLine($"There is {viablePairs} viable pairs");

            Console.WriteLine($"  -Glædelig jul!");
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(
                           t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 })
            );
        }
    }
}