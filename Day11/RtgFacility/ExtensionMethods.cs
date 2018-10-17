using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool IsEmpty<T>(this IEnumerable<T> lst)
        {
            return lst.Count() == 0;
        }

        public static State GetInitialState(this string[] str)
        {
            var state = new State
            {
                Elevator = 0,
                Moves = 0,
                Components = new Dictionary<int, List<Component>>()
            };

            var lines = str.Select(x => x.Split("contains")[1])
                           .Select(x => x.Split(" a ", StringSplitOptions.RemoveEmptyEntries)).ToArray();

            for(var i=0; i<lines.Length; i++)
            {
                var chips = lines[i]
                                .Where(x => x.Contains("chip"))
                                .Select(chip => chip.Split('-')[0])
                                .Select(chip => new Component { Name = chip, Type = ComponentType.Chip });

                var generators = lines[i]
                                    .Where(x => x.Contains("generator"))
                                    .Select(generator => generator.Split(' ')[0])
                                    .Select(generator => new Component { Name = generator, Type = ComponentType.Generator });

                state.Components.Add(i, chips.Concat(generators).ToList());
            }

            return state;
        }
    }
}