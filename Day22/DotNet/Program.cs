using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNet
{
    public class Node
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Avail { get; set; }
        public int Use { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 22 |");
            Console.WriteLine("+-------------------------+");

            var nodes = File
                .ReadAllLines("input.txt")
                .Skip(2)
                .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new Node
                {
                    Name = x[0],
                    X = int.Parse(x[0].Split('-')[1].TrimStart('x')),
                    Y = int.Parse(x[0].Split('-')[2].TrimStart('y')),
                    Size = int.Parse(x[1].TrimEnd('T')),
                    Used = int.Parse(x[2].TrimEnd('T')),
                    Avail = int.Parse(x[3].TrimEnd('T')),
                    Use = int.Parse(x[4].TrimEnd('%')),
                })
                .ToList();

            // Part 1
            var viablePairs = GetPermutations(nodes, 2)
              .Select(x => x.ToList())
              .Count(x =>
                x[0].Used > 0 &&
                x[0].Used <= x[1].Avail
            );

            Console.WriteLine($"There is {viablePairs} viable pairs");
            Console.WriteLine();

            // Part 2
            var maxX = nodes.Max(x => x.X);
            var maxY = nodes.Max(y => y.Y);

            var map = new Node[maxX + 1, maxY + 1];

            foreach (var node in nodes)
            {
                map[node.X, node.Y] = node;
            }

            var target = map[maxX, 0];

            for (var y = 0; y < maxY + 1; ++y)
            {
                for (var x = 0; x < maxX + 1; ++x)
                {
                    var e = map[x, y];

                    if (e.X == 0 && e.Y == 0) // Is goal pos
                    {
                        Console.Write($"G ");
                    }
                    else if (target.Name == e.Name) // Is target
                    {
                        Console.Write($"T ");
                    }
                    else if (e.Used == 0) // Is empty space
                    {
                        Console.Write($"_ ");
                    }
                    else
                    {
                        // 1. Check if we're next to an boundary
                        // 2. If not then determine if we can swap data with every adjacent node
                        if (
                          (0 < e.X ? map[e.X - 1, e.Y].Size >= e.Used : true) &&
                          (e.X < maxX ? map[e.X + 1, e.Y].Size >= e.Used : true) &&
                          (0 < e.Y ? map[e.X, e.Y - 1].Size >= e.Used : true) &&
                          (e.Y < maxY ? map[e.X, e.Y + 1].Size >= e.Used : true))
                        {
                            // Swap is possible
                            Console.Write($". ");
                        }
                        else
                        {
                            // Swap is not possible
                            Console.Write($"# ");
                        }
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine($"Just count the number of swaps needed to make the target hit the goal");
            Console.WriteLine($" _ = empty node without data");
            Console.WriteLine($" . = node which can be swapped in all directions");
            Console.WriteLine($" # = node which cannot be swapped in all directions");
            Console.WriteLine($" T = target node with our precious data");
            Console.WriteLine($" G = goal node which we can read from");
            Console.WriteLine();

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