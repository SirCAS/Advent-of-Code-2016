using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day07.ConsoleApplication
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 07 |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine();

            const string DataFile = "input.txt";

            var splits =  File
                .ReadAllLines(DataFile)
                .Select(x => Regex.Split(x, @"(\[[a-z]+\])"));

            var tlsSupport = splits
                .Where(x => !x.Any(y => y.Contains("[") && HasAbba(y))) // Remove lines with ABBA in brackets
                .Where(x => x.Any(y => HasAbba(y))) // Only use lines with has atleast one ABBA
                .ToList();
            Console.WriteLine($"There is {tlsSupport.Count} lines which supports TLS (transport-layer snooping)");

            var sslSupport = splits.Where(x => HasSsl(x)).ToList();
            Console.WriteLine($"There is {sslSupport.Count} lines which supports SSL (super-secret listening)");

            Console.WriteLine($"  -Glædelig jul!");
        }


        private static bool HasSsl(string[] line)
        {
            var superNetsAbaBab = GetAbaBab(line.Where(x => !x.Contains("["))); // outside
            var hyperNetsAbaBab = GetAbaBab(line.Where(x => x.Contains("["))); // inside bracket
            return superNetsAbaBab.Any(x => hyperNetsAbaBab.Any(y => y[0] == x[1] && y[1] == x[0] && y[0] != x[0]));
        }

        private static IEnumerable<string> GetAbaBab(IEnumerable<string> line)
        {
            var result = new List<string>();
            foreach(var substring in line)
            {
                for(int x=0; x < substring.Length-2; ++x)
                {
                    if(substring[x] != substring[x+1] && substring[x] == substring[x+2])
                        result.Add(new string(new[] { substring[x], substring[x+1], substring[x+2] }));
                }
            }
            return result;
        }

        private static bool HasAbba(string chunck)
        {
            for(int x=0; x<chunck.Length-3; ++x)
                if(chunck[x] == chunck[x+3] && chunck[x+1] == chunck[x+2] && chunck[x] != chunck[x+1])
                    return true;

            return false;
        }
    }
}
