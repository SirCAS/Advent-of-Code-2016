using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RtgFacility
{
    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            return x.Hash.Equals(y.Hash);
        }

        public int GetHashCode(State obj)
        {
            return obj.Hash.GetHashCode();
        }
    }
}
