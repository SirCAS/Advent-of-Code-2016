using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day06.ConsoleApplication
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 06 |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine();

            // LINQ only solution (yea, I know... One thing took the other and suddenly the solution was all LINQ'fied)
            var result = Enumerable
                .Range(0, 8)
                .Select(col =>
                    new {
                        col = col,
                        chars = File.ReadAllLines("input.txt").Select(x => x[col])
                    }
                )
                .Select(col =>
                    col.chars.GroupBy(u => u)
                             .Select(g => new { letter = g.Key, count = g.Count()})
                             .OrderByDescending(o => o.count)
                )
                .Select(col =>
                    new {
                        max = col.First().letter.ToString(),
                        min = col.Last().letter.ToString()
                    }
                )
                .Aggregate((a, b) =>
                    new {
                        max = a.max + b.max,
                        min = a.min + b.min
                    }
                );

            Console.WriteLine($"Message using most occurences  : {result.max}");
            Console.WriteLine($"Message using least occurences : {result.min}");
            Console.WriteLine();

            // End of LINQ solution and start of readable solution for the faint hearted
            var input = File.ReadAllLines("input.txt");
            var data = input.First().Select(x => new Dictionary<char, int>()).ToList();

            foreach(var line in input)
            {
                for(int x=0; x<line.Length; ++x)
                {
                    var dict = data[x];
                    var cha = line[x];
                    if(dict.ContainsKey(cha))
                    {
                        ++dict[cha];
                    } else {
                        dict.Add(cha, 1);
                    }
                }
            }

            var most = new StringBuilder();
            var least = new StringBuilder();

            foreach(var col in data)
            {
                var ordered = col.OrderByDescending(x => x.Value);
                most.Append(ordered.First().Key);
                least.Append(ordered.Last().Key);
            }

            Console.WriteLine($"Most is {most} and least is {least}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}