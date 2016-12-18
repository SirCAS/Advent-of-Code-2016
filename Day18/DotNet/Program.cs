using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day18
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 18 |");
            Console.WriteLine("+-------------------------+");

            //const int rows = 40; // Part 1
            const int rows = 400000; // Part 2
            const string input = "^..^^.^^^..^^.^...^^^^^....^.^..^^^.^.^.^^...^.^.^.^.^^.....^.^^.^.^.^.^.^.^^..^^^^^...^.....^....^.";

            //const int rows = 10;
            //const string input = ".^^.^.^^^^";

            var map = new List<string>{ input };
            for(int x=0; x<rows-1; ++x)
                map.Add(NextRow(map[x]));

            var safeTitles = map.Sum(x => x.Count(y => y == '.'));
            Console.WriteLine($"There is {safeTitles} safe titles"); // 2469 // 2420

            Console.WriteLine($"  -Glædelig jul!");
        }

        private static string NextRow(string row)
        {
            var strBuilder = new StringBuilder();            
            strBuilder.Append(GetType('.', row[0], row[1]));
            for(int x=1; x < row.Length - 1; ++x)
                strBuilder.Append(GetType(row[x-1], row[x], row[x+1]));
            strBuilder.Append(GetType(row[row.Length-2], row[row.Length - 1], '.'));
            return strBuilder.ToString();
        }

        private static char GetType(char l, char c, char r)
        {
            if(l == '^' && c == '^' && r == '.') return '^';
            if(l == '.' && c == '^' && r == '^') return '^';
            if(l == '^' && c == '.' && r == '.') return '^';
            if(l == '.' && c == '.' && r == '^') return '^';
            return '.';
        }
    }
}