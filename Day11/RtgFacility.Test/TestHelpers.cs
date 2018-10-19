using System.Collections.Generic;
using System.Linq;

namespace RtgFacility.Test
{
    public static class TestHelpers
    {
        public static State GetState(this string str)
        {
            return new State
            {
                Elevator = str.Find('E', -2).ToInt() - 1,  // Zero indexed
                Components = str.Split('\n')
                                  .ToDictionary(
                                        f => f.Find('F', 1).ToInt() - 1, // Zero indexed
                                        f => f.FindAll('G', -1)
                                            .Select(x => "G" + x )
                                            .Concat(f.FindAll('M', -1)
                                                    .Select(x => "C" + x))
                                            .ToList()
                                        )
            };
        }

        public static List<string> FindAll(this string str, char let, int offset)
        {
            var charSplits = str.Split(let).ToList();

            if(charSplits.Count > 1)
            {
                return charSplits.Where(x => x.Length > 1)
                                 .Select(x => x.Substring(x.Length + offset, 1).Trim())
                                 .Where(x => !string.IsNullOrWhiteSpace(x))
                                 .ToList();
            }

            return new List<string>();
        }

        public static char Find(this string str, char let, int offset)
        {
            return str[str.IndexOf(let) + offset];
        }

        public static int ToInt(this char i)
        {
            return (int)char.GetNumericValue(i);
        }
    }
}

