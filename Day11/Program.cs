using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public class Floor
    {
        public int Index { get; set; }

        public List<string> Chips { get; set; }

        public List<string> Generators { get; set; }
    }

    public class State
    {
        // TODO: Implement equals method
        public IDictionary<int, Floor> Floors { get; set; }
        public int Elevator { get; set; }
        public int Moves { get; set; }

        /*
            Chips is fried if with foregin generator (regardless if foregin generator is connected to a chip)
            Bring all items to 4th floor
            Elevator has capacity of 2 items
            Elevator only moves with atleast 1 item
            Each elevator stop counts as one step, even if nothing is added to or removed from it
        */
        public IList<State> GetNextMoves()
        {
            return new List<State>();
        }

        public bool IsFinished()
        {
            return Elevator == 4 && Floors.Where(x => x.Key != 4)
                                          .All(x => x.Value.Chips.Count == 0 && x.Value.Generators.Count == 0);
        }
    }

    public static class ExtensionMethods
    {
        public static State GetInitialState(this string[] str)
        {
            return new State
            {
                Elevator = 0,
                Moves = 0,
                Floors = str
                    .Select(x => x.Split("contains")[1])
                    .Select(x => x.Split(" a ", StringSplitOptions.RemoveEmptyEntries))
                    .Select((floorContent, floorNumber) => new Floor
                    {
                        Index = floorNumber,
                        
                        Chips = floorContent.Where(x => x.Contains("chip"))
                                            .Select(chip => chip.Split('-')[0])
                                            .ToList(),
                        
                        Generators = floorContent.Where(x => x.Contains("generator"))
                                                 .Select(generator => generator.Split(' ')[0])
                                                 .ToList()
                    })
                    .ToDictionary(y => y.Index, y => y),
            };
        }
    }

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

