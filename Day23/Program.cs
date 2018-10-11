using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day23
{
    public struct Cmd
    {
        public const string COPY = "cpy";
        public const string INCREMENT = "inc";
        public const string DECREMENT = "dec";
        public const string JUMP = "jnz";
        public const string TOGGLE = "tgl";
    }

    struct Instruction
    {
        public string Full { get; set; }
        public string Cmd { get; set; }
        public string[] Args { get; set; }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 23 |");
            Console.WriteLine("+-------------------------+");

            var instructions = File.ReadAllLines("input.txt")
                .Select(x => x.Split(' '))
                .Select(x => new Instruction
                {
                    Cmd = x.First(),
                    Args = x.Skip(1).ToArray()
                }).ToArray();

            var part1 = GetValues(new Dictionary<string, int>
            {
                {"a", 7}, {"b", 0}, {"c", 0}, {"d", 0}
            }, instructions.Clone() as Instruction[]);

            Console.WriteLine($"Part 1 : a={part1["a"]}");

            var part2 = GetValues(new Dictionary<string, int>
            {
                {"a", 12}, {"b", 0}, {"c", 0}, {"d", 0}
            }, instructions.Clone() as Instruction[]);

            Console.WriteLine($"Part 2 : a={part2["a"]}");

            Console.WriteLine($"  -Glædelig jul!");
        }    

        private static IDictionary<string, int> GetValues(IDictionary<string, int> registers, Instruction[] instructions)
        {
            int sp = 0;
            while (sp < instructions.Length)
            {
                var instruction = instructions[sp];
                
                switch (instruction.Cmd)
                {
                    case Cmd.COPY:
                        int value;
                        if (int.TryParse(instruction.Args[0], out value))
                        {
                            registers[instruction.Args[1]] = value;
                        }
                        else
                        {
                            registers[instruction.Args[1]] = registers[instruction.Args[0]];
                        }
                        break;

                    case Cmd.INCREMENT:
                        registers[instruction.Args[0]]++;
                        break;

                    case Cmd.DECREMENT:
                        registers[instruction.Args[0]]--;
                        break;

                    case Cmd.JUMP:
                        int x;
                        if(!int.TryParse(instruction.Args[0], out x))
                        {
                            x = registers[instruction.Args[0]];
                        }
                        if(x != 0)
                        {
                            int y;
                            if(!int.TryParse(instruction.Args[1], out y))
                            {
                                y = registers[instruction.Args[1]];
                            }

                            sp += y;
                            continue;
                        }
                        break;

                    case Cmd.TOGGLE:
                        var toggleIndex = sp + registers[instruction.Args[0]];

                        if(toggleIndex < instructions.Length)
                        {
                            if (instructions[toggleIndex].Args.Length == 1)
                            {
                                instructions[toggleIndex].Cmd = (instructions[toggleIndex].Cmd == Cmd.INCREMENT) ? Cmd.DECREMENT : Cmd.INCREMENT;
                            }
                            else
                            {
                                instructions[toggleIndex].Cmd = (instructions[toggleIndex].Cmd == Cmd.JUMP) ? Cmd.COPY : Cmd.JUMP;
                            }
                        }
                        break;
                    
                    default:
                        throw new NotSupportedException();
                }

                ++sp;
            }

            return registers;
        }
    }
}

