using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace RtgFacility
{
    public class State
    {
        public IDictionary<int, List<string>> Components { get; set; }
        public int Elevator { get; set; }
        public int Moves { get; set; }

        public State DeepCopy()
        {
            var copy = this.MemberwiseClone() as State;
            copy.Components = Components.ToDictionary(x => x.Key, x => x.Value.ToList());
            return copy;
        }
    }
}