using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Day09
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 09 |");
            Console.WriteLine("+-------------------------+");

            var input = File.ReadAllText("input.txt");
            
            var decompressor1 = new DecompressorVersion1(input);
            var decompressor2 = new DecompressorVersion2(input);

            Console.WriteLine($"Length of decompressed v1 string is {decompressor1.Length}");
            Console.WriteLine($"Length of decompressed v2 string is {decompressor2.Length}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}

