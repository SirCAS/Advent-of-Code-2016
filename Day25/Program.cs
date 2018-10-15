using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day25
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 25 |");
            Console.WriteLine("+-------------------------+");

            var instructions = File.ReadAllLines("input.txt")
                .Select(x => x.Split(' '))
                .Select(x => new Instruction
                {
                    Cmd = x.First(),
                    Args = x.Skip(1).ToArray()
                }).ToArray();


            // Allow adjusting the instructions to execute before evaluating the result
            var maxIterations = 50000;

            int[] sequence;
            var a = 0;
            do
            {
                sequence = RunInstructions(a, instructions.Clone() as Instruction[], maxIterations);

                Console.WriteLine($"{a:D5} : Len={sequence.Length:D3}, Sequence={string.Join(' ', sequence)}");

                a++;

            }
            while (!SequenceFound(sequence));


            Console.WriteLine("  -Glædelig jul!");
        }

        private static int[] RunInstructions(int a, Instruction[] instructions, int iterationsToRun)
        {
            var registers = new Dictionary<string, int>
            {
                {"a", a}, {"b", 0}, {"c", 0}, {"d", 0}
            };

            var output = new List<int>();

            int stackPointer = 0, iterations = 0;

            while (stackPointer < instructions.Length && iterations < iterationsToRun)
            {
                var instruction = instructions[stackPointer];
                
                switch (instruction.Cmd)
                {
                    case Cmd.COPY:
                        if (int.TryParse(instruction.Args[0], out var value))
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
                        if(!int.TryParse(instruction.Args[0], out var x))
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

                            stackPointer += y;
                            continue;
                        }
                        break;

                    case Cmd.TOGGLE:
                        var toggleIndex = stackPointer + registers[instruction.Args[0]];

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
                    
                    case Cmd.OUT:
                        if (!int.TryParse(instruction.Args[0], out var outValue))
                        {
                            outValue = registers[instruction.Args[0]];
                        }

                        // Early exit as only 0 and 1 are allowed output
                        if (outValue == 0 || outValue == 1)
                        {
                            output.Add(outValue);
                        }
                        else
                        {
                            return null;
                        }

                        break;

                    default:
                        throw new NotSupportedException();
                }

                ++stackPointer;
                ++iterations;
            }

            return output.ToArray();
        }

        private static bool SequenceFound(int[] seq)
        {
            if (seq.Length < 2 || seq[0] == seq[1])
            {
                return false;
            }

            for (var x = 0; x < seq.Length; x++)
            {
                if (x % 2 == 0)
                {
                    if (seq[x] != seq[0])
                    {
                        return false;
                    }
                }
                else
                {
                    if (seq[x] != seq[1])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

