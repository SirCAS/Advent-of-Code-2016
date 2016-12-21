using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 21 |");
            Console.WriteLine("+-------------------------+");

            var instructions = File.ReadAllLines("input.txt").ToList();

            { // Part 1 - encode password
                var password = "abcdefgh";
                var encodedPassword = EncodePassword(password, instructions);
                Console.WriteLine($"{password} encoded is {encodedPassword}");
            }

            { // Part 2 - brute force password from encoded password iterating through all permutations
                var encodedPassword = "fbgdceah";
                var permutations = GetPermutations(encodedPassword, encodedPassword.Length).Select(x => new string(x.ToArray()));
                var password = permutations.First(x => EncodePassword(x, instructions) == encodedPassword);
                Console.WriteLine($"{password} encoded is {encodedPassword}");
            }

            Console.WriteLine($"  -Glædelig jul!");
        }

        private static string EncodePassword(string pass, IList<string> instructions)
        {
            // Console.WriteLine($"{pass} (Raw input)");
            var work = instructions
                .Select(instruction => {
                    string[] x = instruction.Split(' ');
                    switch(x[0]+x[1])
                    {
                        case "swapposition": {
                            int p1 = int.Parse(x[2]);
                            int p2 = int.Parse(x[5]);
                            var tmp = pass.ToCharArray();
                            tmp[p1] = pass[p2];
                            tmp[p2] = pass[p1];
                            pass = new string(tmp);
                        }
                        break;
                        case "swapletter": {
                            var l1 = x[2][0];
                            var l2 = x[5][0];
                            pass = pass.Replace(l1, '#').Replace(l2, l1).Replace('#', l2);
                        }
                        break;
                        case "rotateleft": {
                            int steps = int.Parse(x[2]) % pass.Length;
                            pass = pass.Substring(steps, pass.Length - steps) + pass.Substring(0, steps);
                        }
                        break;
                        case "rotateright": {
                            int split = pass.Length - int.Parse(x[2]) % pass.Length;
                            pass = pass.Substring(split) + pass.Substring(0, split);
                        }
                        break;
                        case "rotatebased": {
                            int steps = pass.IndexOf(x[6]);
                            steps = (steps>=4 ? steps + 2 : steps + 1) % pass.Length;

                            int split = pass.Length - steps;
                            pass = pass.Substring(split) + pass.Substring(0, split);
                        }
                        break;
                        case "reversepositions": {
                            int p1 = int.Parse(x[2]);
                            int p2 = int.Parse(x[4]) + 1;
                            var reversed = new string(pass.Substring(p1, p2 - p1).Reverse().ToArray());
                            pass = pass.Remove(p1, p2-p1).Insert(p1, reversed);
                        }
                        break;
                        case "moveposition": {
                            var p1 = int.Parse(x[2]);
                            var p2 = int.Parse(x[5]);
                            var move = new string(new [] { pass[p1] });
                            pass = pass.Remove(p1, 1).Insert(p2, move);
                        }
                        break;
                    }
                    return new {
                        Instruction = instruction,
                        Result = pass
                    };
                })
                .ToList();
            
            //foreach(var step in work) Console.WriteLine($"{step.Result} ({step.Instruction})");

            return pass;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(
                           t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 })
            );
        }
    }
}