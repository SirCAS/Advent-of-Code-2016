using System.Collections.Generic;
using System.Linq;

namespace RtgFacility.Test
{
    public static class TestHelpers
    {
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

