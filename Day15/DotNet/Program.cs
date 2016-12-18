using System;
using System.IO;
using System.Linq;

namespace Day15
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 15 |");
            Console.WriteLine("+-------------------------+");

            var input = File
                .ReadAllLines("input.part2.txt")
                .Select(x => x.Split(' '))
                .Select(x => new {
                    Disc = ulong.Parse(x[1].Substring(1, x[1].Length - 1)),
                    Pos = ulong.Parse(x[3]),
                    Offset = ulong.Parse(x[11].Substring(0, x[11].Length - 1))
                })
                .ToList();

            for(ulong t=0; t<10000000; ++t)
            {
                int match = 0;
                foreach(var disc in input)
                {
                    if((disc.Disc + t + disc.Offset) % disc.Pos != 0)
                        break;
                    ++match;
                }

                if(match == input.Count)
                {
                    Console.WriteLine($"First lineup is at: t={t}");
                    break;
                }
            }

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}