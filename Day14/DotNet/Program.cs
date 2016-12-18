using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Day14
{
    public class OneTimePadGenerator
    {
        private string salt;
        private MD5 md5;
        private Queue<string> hashes;
        private List<string> keys;
        private int index;
        private int iterations;

        public OneTimePadGenerator(string salt, int iterations)
        {
            this.salt = salt;
            this.iterations = iterations;
            this.md5 = MD5.Create();
            this.keys = new List<string>();
            this.hashes = new Queue<string>(1000);
            this.index = 0;
        }

        private string ApplyMd5(string input)
        {
            StringBuilder sb = new StringBuilder();

            var t2 = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            
            foreach (byte o in t2)
                sb.Append(o.ToString("X2"));

            return sb.ToString().ToLower();
        }

        private string GetNextHash()
        {
            var tmp = salt + index++;
            for(int x=0; x<iterations; x++)
            {
                tmp = ApplyMd5(tmp);
            }

            return tmp;
        }

        public int GetIndexForKey(int keyno)
        {
            for(int x=0; x<1000; ++x)
            {
                var t1 = GetNextHash();
                hashes.Enqueue(t1);
            }

            var regex = new Regex(@"([a-z\d])\1\1");
            while(keys.Count < keyno)
            {
                hashes.Enqueue(GetNextHash());
                var hash = hashes.Dequeue();
                var m = regex.Match(hash);
                if(m.Success)
                {
                    var fiveSequenceMatch = new string(m.Value[0], 5);
                    if(hashes.Where(x => x.Contains(fiveSequenceMatch)).Any())
                    {
                        keys.Add(hash);
                    }
                }
            }

            return index - 1001; // Subtract queue size to obtain actual index
        }
    }

    public class Program
    { 
        public static void Main(string[] args)
        {
            Console.WriteLine("+-------------------------+");
            Console.WriteLine("| Advent of Code - Day 14 |");
            Console.WriteLine("+-------------------------+");

            const string salt = "jlmsuwbz";
            const int keyNo = 64;
            const int iterations = 2017; // Use a value of 1 is for part 1

            var generator = new OneTimePadGenerator(salt, iterations);

            var index = generator.GetIndexForKey(keyNo);

            Console.WriteLine($"Index for the {keyNo}th key is {index}");

            Console.WriteLine($"  -Glædelig jul!");
        }
    }
}