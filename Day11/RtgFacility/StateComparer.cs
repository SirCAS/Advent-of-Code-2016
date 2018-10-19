using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RtgFacility
{
    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return Translate(x) == Translate(y);
        }

        public int GetHashCode(State obj)
        {
            var state = obj as State;
            return Translate(state).GetHashCode();
        }


        /* public int GetHashCode(State obj)
        {
            var state = obj as State;

            unchecked
            {
                var hash = state.Elevator;
                foreach(var x in state.Components)
                {
                    hash ^= x.Value.Count;
                }
                return hash;
            }
        } */

        /* private string Translate(State state)
        {
            StringBuilder str = new StringBuilder();
            str.Append(state.Elevator);
            foreach (var level in state.Components)
            {
                str.Append(level.Key);
                
                var values = level.Value
                    .GroupBy(x => x.Substring(1))
                    .SelectMany(x => {
                        if(x.Count() > 1)
                        {
                            return x.Select(y => y.Substring(0, 1) + "PA");
                        }
                        return x;
                    })
                    .OrderBy(x => x);

                str.Append(string.Concat(values));
            }
            return str.ToString();
        }*/

        private string Translate(State state)
        {
            StringBuilder str = new StringBuilder();
            str.Append(state.Elevator);
            foreach (var level in state.Components)
            {
                str.Append(level.Key);
                str.Append(string.Concat(level.Value.OrderBy(x => x)));
            }
            return str.ToString();
        }
    }
}
