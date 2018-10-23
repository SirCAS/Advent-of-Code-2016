using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtgFacility
{
    public static class ExtensionMethods
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                        .SelectMany(t =>
                            list.Where(e => !t.Contains(e)),
                            (t1, t2) => t1.Concat(new T[] { t2 })
                        );
        }

        public static string ToPrettyString(this State state)
        {
            StringBuilder str = new StringBuilder();

            var ordered = state.Components.OrderBy(x => x.Key).Select(x => x.Value.ToArray()).ToArray();

            var maxCols = ordered.Max(x => x.Count());

            for (int x = ordered.Length - 1; x > -1; x--)
            {
                str.Append($"F{x} ");
                if (x == state.Elevator)
                {
                    str.Append("E ");
                }
                else
                {
                    str.Append(". ");
                }

                for (int y = 0; y < maxCols; y++)
                {
                    if (ordered[x].Count() > y)
                    {
                        str.Append(ordered[x][y]);
                        str.Append(" ");
                    }
                    else
                    {
                        str.Append(".   ");
                    }
                }
                str.AppendLine();
            }

            return str.ToString();
        }

        public static State GetInitialState(this string[] str)
        {
            var components = new Dictionary<int, HashSet<string>>();

            var lines = str.Select(x => x.Split("contains")[1])
                           .Select(x => x.Split(" a ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

            for(var i=0; i<lines.Length; i++)
            {
                var chips = lines[i]
                                .Where(x => x.Contains("chip"))
                                .Select(chip => chip.Split('-')[0])
                                .Select(chip => "C" + chip.Substring(0, 2).ToUpperInvariant());

                var generators = lines[i]
                                    .Where(x => x.Contains("generator"))
                                    .Select(generator => generator.Split(' ')[0])
                                    .Select(generator => "G" + generator.Substring(0, 2).ToUpperInvariant());

                components.Add(i, chips.Concat(generators).ToHashSet());
            }

            return new State(0, 0, components);
        }
    }
}