using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RtgFacility
{
    public class StateWatcher
    {
        private HashSet<string> content = new HashSet<string>();
        public void Add(State state)
        {
            content.Add(Translate(state));
        }

        public bool Contains(State state)
        {
            return content.Contains(Translate(state));
        }

        public int Count => content.Count;

        public string Translate(State state)
        {
            StringBuilder str = new StringBuilder();
            str.Append(state.Elevator);
            str.Append('|');
            foreach (var level in state.Components)
            {
                str.Append(level.Key);
                str.Append(':');



                foreach (var component in level.Value.OrderBy(x => x.Type).ThenBy(x => x.Name))
                {
                    str.Append(component.Name);
                    str.Append('.');
                    str.Append(component.Type);
                    str.Append('!');
                }
            }
            return str.ToString();
        }

        public string Prettify(State state)
        {
            StringBuilder str = new StringBuilder();
            
            var ordered = state.Components.OrderBy(x => x.Key).Select(x => x.Value).ToArray();

            for(int x=ordered.Length - 1; x > -1; x--)
            {
                str.Append($"F{x} ");
                if(x == state.Elevator)
                {
                    str.Append("E ");
                } else {
                    str.Append(". ");
                }

                for(int y=0; y<10; y++)
                {
                    if(ordered[x].Count > y)
                    {
                        str.Append(ordered[x][y].Name.Substring(0, 2).ToUpperInvariant());
                        str.Append(ordered[x][y].Type == ComponentType.Chip ? "C" : "G");
                        str.Append(" ");
                    } else {
                        str.Append(".   ");
                    }
                }
                str.AppendLine();

            }

            return str.ToString();

        }
    }
}