using System;
using System.Collections.Generic;
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
                .ReadAllLines("input.txt")
                .ToArray()
                .GetInitialState();

            var visited = new HashSet<State>();
            visited.Add(initialState);

            var frontier = new Queue<State>();
            frontier.Enqueue(initialState);

            while(frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if(current.IsFinished())
                {
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

            Console.WriteLine("  -Glædelig jul!");
        }
    }
}

