using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RtgFacility
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 11 |");
            Console.WriteLine("+-------------------------+");

            var initialState = File
                .ReadAllLines("input.part2.txt")
                .ToArray()
                .GetInitialState();

            var sw = new Stopwatch();
            
            sw.Start();

            var visited = new StateWatcher();
            visited.Add(initialState);

            var frontier = new Queue<State>();
            frontier.Enqueue(initialState);

            var x = 0;
            var df = 0;
            var dv = 0;
            while(frontier.Count > 0)
            {
                if(++x % 10000 == 0)
                {
                    Console.WriteLine($"Current states is {frontier.Count:D10} ({(frontier.Count-df):D6}) and visited {visited.Count:D10} ({(visited.Count-dv):D6})");
                    df = frontier.Count;
                    dv = visited.Count;
                }
                    

                var current = frontier.Dequeue();

                //Console.WriteLine(new StateWatcher().Prettify(current));

                if(current.IsFinished())
                {
                    Console.WriteLine($"Found solution with {current.Moves}");
                    break;
                }

                foreach(var next in current.GetNextMoves())
                {
                    if(!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }

            sw.Stop();

            Console.WriteLine($"Simulation took {sw.Elapsed.TotalMinutes} minutes");

            Console.WriteLine("  -Glædelig jul!");
        }
    }
}

