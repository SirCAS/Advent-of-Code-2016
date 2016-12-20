using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    public class Range
    {
        public uint From { get; set;}
        public uint To { get; set;} 
    }

    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 20 |");
            Console.WriteLine("+-------------------------+");

            const uint maxValue = 4294967295;

            var input = File.ReadAllLines("input.txt")
                .Select(x => x.Split('-'))
                .Select(x => new Range {
                    From = uint.Parse(x[0]),
                    To = uint.Parse(x[1]),
                })
                .OrderBy(x => x.From)
                .ToList();

            var ranges = new List<Range> { input[0] };

            for (int x=1; x<input.Count; ++x)
            {
                var prev = ranges.Last();
                if(input[x].From < prev.To)
                {
                    if(prev.To < input[x].To)
                        prev.To = input[x].To;
                } else {
                    ranges.Add(input[x]);
                }
            }

            uint? lowest = null;
            uint allowedCount = 0;
            uint prevMin = ranges[0].From;
            foreach(var range in ranges)
            {
                if(range.From != prevMin)
                {
                    var diff = range.From - prevMin - 1;
                    if(0 < diff && !lowest.HasValue)
                        lowest = prevMin + 1;

                    allowedCount += diff;
                }

                prevMin = range.To;
            }

            if(prevMin < maxValue)
                allowedCount += maxValue - prevMin;

            Console.WriteLine($"Lowest IP is {lowest}");
            Console.WriteLine($"Total allowed IPs is {allowedCount}");
            
            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}