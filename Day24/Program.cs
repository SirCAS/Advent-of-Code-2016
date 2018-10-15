using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 24 |");
            Console.WriteLine("+-------------------------+");

            var data = File.ReadAllLines("input.txt").ToArray();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            var graph = new Graph(data);
            var start = graph.GetStart();
            var destinations = graph.GetDestinations();

            uint minRoute = uint.MaxValue;

            // Create list with all possible routes excluding our starting position (E.g. -> 3 -> 1 -> 4)
            var routes = GetPermutations(destinations, destinations.Count).ToList();
            var total = routes.Count;

            //Console.WriteLine($"Found {total} routes");

            for(var r = 0; r <total; r++)
            {
                var routeList = routes[r].ToArray();
                uint routeLength = 0;
                for (int x = 0; x < routeList.Length; x++)
                {
                    if (x == 0)
                    {
                        routeLength += graph.GetRoute(start, routeList[0]).GetLength();
                    }
                    else
                    {
                        routeLength += graph.GetRoute(routeList[x-1], routeList[x]).GetLength();
                    }

                    if(x == routeList.Length - 1)
                    {
                        routeLength += graph.GetRoute(routeList[x], start).GetLength();
                    }


                    if (routeLength > minRoute)
                    {
                        break;
                    }
                }

                if (minRoute > routeLength)
                {
                    minRoute = routeLength;
                }

                /*if (r % 100 == 0)
                {
                    Console.WriteLine($"{r}/{total} : Min {minRoute}");
                }*/
            }

            stopwatch.Stop();

            Console.WriteLine($"Route length is {minRoute} and was found in {stopwatch.Elapsed.TotalSeconds}");

            Console.Read();

            Console.WriteLine($"  -Glædelig jul!");
        }


        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}

