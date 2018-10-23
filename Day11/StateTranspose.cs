using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtgFacility
{
    public static class StateTranspose
    {
        public static string GetHash(int elevator, IDictionary<int, HashSet<string>> components)
        {
            StringBuilder str = new StringBuilder();
            str.Append(elevator);
            foreach (var level in components)
            {
                str.Append(level.Key);

                var pairs = level
                    .Value
                    .GroupBy(x => x.Substring(1))
                    .Where(group => group.Count() > 1)
                    .SelectMany(x => x)
                    .ToHashSet();
                
                str.Append(new string('@', pairs.Count));

                var input = level.Value.Except(pairs);
                str.Append(string.Concat(input.OrderBy(x => x)));
            }
            return str.ToString();
        }
    }
}
