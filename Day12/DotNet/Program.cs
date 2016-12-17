using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 12 |");
            Console.WriteLine("+-------------------------+");

            var instructions = File.ReadAllLines("input.txt")
                .Select(x => x.Split(' '))
                .Select(x => {
                    switch(x[0])
                    {
                        case "cpy": return new { Ins = "cpy", Val = x[1], Reg = x[2]};
                        case "inc": return new { Ins = "add", Val = "1",  Reg = x[1]};
                        case "dec": return new { Ins = "add", Val = "-1", Reg = x[1]};
                        case "jnz": return new { Ins = "jmp", Val = x[2], Reg = x[1]};
                        default: throw new NotSupportedException();
                    }
                }).ToList();

            var registers = new Dictionary<char, int>
            {
                {'a', 0}, {'b', 0}, {'c', 1}, {'d', 0}
            };

            int sp = 0;
            while(sp < instructions.Count)
            {
                var cmd = instructions[sp];
                switch(cmd.Ins)
                {
                    case "cpy":
                        int value;
                        if(int.TryParse(cmd.Val, out value))
                        {
                            registers[cmd.Reg[0]] = value;
                        } else {
                            registers[cmd.Reg[0]] = registers[cmd.Val[0]];
                        }   
                    break;
                    case "add":
                        registers[cmd.Reg[0]] += int.Parse(cmd.Val);
                        break;
                    case "jmp":
                        int register;
                        if(!int.TryParse(cmd.Reg.ToString(), out register))
                            register = registers[cmd.Reg[0]];

                        if(register != 0)
                        {
                            sp += int.Parse(cmd.Val);
                            continue;
                        }
                        break;
                    default:
                     throw new NotSupportedException();
                }
                ++sp;
            }

            Console.WriteLine($"Value of register a is {registers['a']}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}

