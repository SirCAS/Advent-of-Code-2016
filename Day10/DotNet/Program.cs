using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 10 |");
            Console.WriteLine("+-------------------------+");

            // Read data file
            var data = File.ReadAllLines("input.txt");

            // Get initial values
            var inputs = data
                .Where(x => x.StartsWith("value"))
                .Select(x => x.Split(' '))
                .Select(x => new
                {
                    Bot = int.Parse(x[5]),
                    ChipId = int.Parse(x[1])
                });

            // Get chip flow instructions
            var flows = data
                .Where(x => x.StartsWith("bot"))
                .Select(x => x.Split(' '))
                .ToDictionary(
                    x => int.Parse(x[1]),
                    x => new
                    {
                        LowIsOutput = x[5] == "output",
                        Low = int.Parse(x[6]),
                        HighIsOutput = x[10] == "output",
                        High = int.Parse(x[11])
                    }
                );
            
            // Get bots
            var bots = inputs
                .Select(x => x.Bot)
                .Concat(flows.Select(x => x.Key))
                .Distinct()
                .ToDictionary(
                    x => x,
                    x => new List<int>()
                );

            // Set inital values
            foreach(var input in inputs)
                bots[input.Bot].Add(input.ChipId);

            // Get output bins
            var outputs = flows
                .SelectMany(x => 
                    new [] {
                        x.Value.HighIsOutput?x.Value.High:(int?)null,
                        x.Value.LowIsOutput?x.Value.Low:(int?)null
                    }
                    .Where(y => y.HasValue)
                    .Select(y => y.Value)
                ).ToDictionary(
                    x => x,
                    x => 0
                );

            // Set match values
            var match = new List<int> { 61, 17 };

            // Keep looping untill we have found our match
            while(bots.Any(x => x.Value.Any()))
            {
                foreach (var bot in bots.Where(x => x.Value.Count > 1))
                {
                    var values = bot.Value;
                        //if(values.Count > 2) throw new NotSupportedException();

                    if(values.All(x => match.Contains(x)))
                    {
                        Console.WriteLine(
                            $"Bot {bot.Key} is responsible for comparing " +
                            string.Join(" and ", match.Select(x => x.ToString()))
                        );
                    }

                    var high = values.OrderByDescending(x => x).First();
                    var low = values.OrderByDescending(x => x).Last();

                    var instruction = flows[bot.Key];
                    if(instruction.HighIsOutput)
                        outputs[instruction.High] += high;
                    else
                        bots[instruction.High].Add(high);

                    if(instruction.LowIsOutput)
                        outputs[instruction.Low] += low;
                    else
                        bots[instruction.Low].Add(low);

                    bot.Value.Clear();
                }
            }

            // Calculate product of output bins
            var product = outputs[0] * outputs[1] * outputs[2];
            Console.WriteLine($"Product of bin 0, 1 and 2 is: {product}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}

