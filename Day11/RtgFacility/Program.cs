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
                .ReadAllLines("input.test.txt")
                .ToArray()
                .GetInitialState();

            Console.WriteLine("Initial state:");
            Console.WriteLine(initialState.ToPrettyString());

            var visited = new HashSet<State>(new StateComparer());
            visited.Add(initialState);

            var frontier = new Queue<State>();
            frontier.Enqueue(initialState);

            var stateGenerator = new StateGenerator();

            var sw = new Stopwatch();
            sw.Start();

            var iterations = 0;
            while(frontier.Count > 0)
            {
                if(++iterations % 10000 == 0)
                {
                    Console.WriteLine($"Queued states is {frontier.Count:D10} and evaluated {visited.Count:D10} after {sw.Elapsed.TotalSeconds:F1} seconds");
                }

                var current = frontier.Dequeue();
                if(StateValidator.IsFinalState(current))
                {
                    sw.Stop();
                    Console.WriteLine($"Found solution with {current.Moves} moves after {sw.Elapsed.TotalSeconds:F1} seconds");
                    break;
                }

                foreach(var next in stateGenerator.Next(current))
                {
                    if(visited.Add(next))
                    {
                        frontier.Enqueue(next);
                    }
                }
            }

            Console.WriteLine("  -Glædelig jul!");
        }
    }
}

