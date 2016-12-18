using System;

namespace Day16
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 16 |");
            Console.WriteLine("+-------------------------+");

            var input = "10010000000110000";
            var firstDisklength = 272;
            var secondDiskLength = 35651584;

            var generator = new DataGenerator();
            var disk1Checksum = generator.CalculateDiskChecksum(input, firstDisklength);
            var disk2Checksum = generator.CalculateDiskChecksum(input, secondDiskLength);

            Console.WriteLine($"Checksum is for disk 1 is {disk1Checksum} and {disk2Checksum} for disk 2");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}