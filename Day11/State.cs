using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace RtgFacility
{
    public struct State
    {
        public State(int elevator, int moves, IDictionary<int, HashSet<string>> components)
        {
            this.Components = components;
            this.Elevator = elevator;
            this.Moves = moves;
            this.Hash = StateTranspose.GetHash(elevator, components);
        }
        public IDictionary<int, HashSet<string>> Components { get; }
        public int Elevator { get; }
        public int Moves { get; }
        public string Hash { get; }
    }
}