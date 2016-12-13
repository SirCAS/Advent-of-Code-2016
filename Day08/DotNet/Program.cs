using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day08.ConsoleApplication
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 08 |");
            Console.WriteLine("+-------------------------+");

            const int WIDTH = 50;
            const int HEIGHT = 6;

            var display = new ulong[HEIGHT];

            var stack =  File
                .ReadAllLines("input.txt")
                .Select(x => x.Split(' '))
                .Select(x => new { IsRot = (x[1][0] == 'c' || x[1][0] == 'r'), Values = x })
                .Select(x => new {
                    Operation = x.Values[0] + (x.IsRot ? x.Values[1] : string.Empty),
                    Values = (x.IsRot ?
                        new Tuple<int, int>(int.Parse(x.Values[2].Substring(2)), int.Parse(x.Values[4])) :
                        new Tuple<int, int>(int.Parse(x.Values[1].Split('x')[0]), int.Parse(x.Values[1].Split('x')[1])))
                });

            foreach(var instruction in stack)
            {
                var v1 = instruction.Values.Item1;
                var v2 = instruction.Values.Item2;
                switch(instruction.Operation) {
                    case "rect":
                        for(int y=0; y < v2; ++y)
                            display[y] = display[y] | ~(ulong.MaxValue << v1) << WIDTH - v1;
                        break;

                    case "rotatecolumn":
                        for(int r=0; r < (v2 % WIDTH); ++r) {
                            var copy = display.ToArray();
                            for(int y=0; y < HEIGHT; ++y) {
                                var index = (y == HEIGHT - 1) ? 0 : y + 1;
                                var mask = (ulong)1 << (WIDTH - 1 - v1);
                                var isSet = copy[y] & mask;
                                if(isSet == 0) {
                                    display[index] &= ~mask;
                                } else {
                                    display[index] |= mask;
                                }
                            }
                        }
                        break;

                    case "rotaterow":
                        var s = (int)v2 % WIDTH;
                        display[v1] = (display[v1] & ~(ulong.MaxValue << s)) << (WIDTH - s) | display[v1] >> s;
                        break;

                    default: throw new NotImplementedException();
                }
            }

            StringBuilder strBuild = new StringBuilder();
            for(int y=0; y<HEIGHT; ++y)
                strBuild.AppendLine(
                    Convert.ToString((long) display[y], 2)
                        .PadLeft(WIDTH, '.').Replace('1', '#').Replace('0', '.')
                );

            var result = strBuild.ToString();
            var lightsTurnedOn = result.Count(x => x == '#');

            Console.WriteLine($"There is {lightsTurnedOn} lights");
            Console.Write(result);
            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}