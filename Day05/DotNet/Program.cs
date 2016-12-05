using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Day05.ConsoleApplication
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 05 |");
            Console.WriteLine("+-------------------------+");
            Console.WriteLine();

            //var input = "abc";
            var input = "ugkcyxxp";

            var part1 = DecryptUsingWeakAlgoritem(input);
            Console.WriteLine($"Password is {part1} for part 1");

            var part2 = DecryptUsingBetterAlgoritem(input);
            Console.WriteLine($"Password is {part2} for part 2");

            Console.WriteLine($"  -Gruss Gott");
        }

        private static string DecryptUsingWeakAlgoritem(string input)
        {
            ulong i = 0;
            var result = "";

            while(result.Length < 8)
            {
                var seed = input + i;

                byte[] encodedSeed = new UTF8Encoding().GetBytes(seed);
                byte[] hash = ((HashAlgorithm) MD5.Create()).ComputeHash(encodedSeed);
                string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                bool isValid = true;
                for(int x=0; x<5; ++x)
                {
                    if(encoded[x] != '0')
                    {
                        isValid = false;
                        break;
                    }
                }

                if(isValid)
                {
                    result += encoded[5];
                }

                ++i;
            }

            return result;
        }

        private static string DecryptUsingBetterAlgoritem(string input)
        {
            ulong i = 0;
            char?[] result = new char?[8] { null, null, null, null, null, null, null, null};

            while(result.Any(x => !x.HasValue)) {
                var seed = input + i;

                byte[] encodedSeed = new UTF8Encoding().GetBytes(seed);
                byte[] hash = ((HashAlgorithm) MD5.Create()).ComputeHash(encodedSeed);
                string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                bool hasValidPrefix = true;
                for(int x=0; x<5; ++x) {
                    if(encoded[x] != '0') {
                        hasValidPrefix = false;
                        break;
                    }
                }
                
                if(hasValidPrefix && encoded[5] <= '7' && encoded[5] >= '0') {
                    var pos = int.Parse(encoded[5].ToString());
                    if(!result[pos].HasValue)
                    {
                        result[pos] = encoded[6];
                    }
                }

                ++i;
            }

            return String.Join("",result.Select(x => x.Value.ToString()));
        }
    }
}