using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace RtgFacility
{
    public class State
    {
        public IDictionary<int, List<Component>> Components { get; set; }
        public int Elevator { get; set; }
        public int Moves { get; set; }

        public State DeepCopy()
        {
            var other = this.MemberwiseClone() as State;
            other.Components = Components.ToDictionary(
                x => x.Key,
                x => x.Value.Select(y => new Component{ Name = y.Name, Type = y.Type})
                            .ToList());
            return other;
        }

        public IList<State> GetNextMoves()
        {
            var result = new List<State>();
            var currentFloor = Components[Elevator];
            var oneItems = currentFloor.Select(x => new List<Component> { x });
            var twoItems = currentFloor.GetPermutations(2);
            var itemPairs = oneItems.Concat(twoItems).Select(x => x.ToList()).ToList();

            foreach(var itemPair in itemPairs)
            {
                var upState = this.DeepCopy();
                upState.Moves++;
                
                var downState = this.DeepCopy();
                downState.Moves++;

                foreach(var item in itemPair)
                {
                    upState.Components[upState.Elevator].RemoveAll(x => x.Name == item.Name && x.Type == item.Type);
                    downState.Components[downState.Elevator].RemoveAll(x => x.Name == item.Name && x.Type == item.Type);
                    
                    if(Elevator < 3) // Move up
                    {
                        upState.Components[upState.Elevator + 1].Add(item);
                    }
                    
                    if(Elevator > 0) // Move down
                    {
                        downState.Components[downState.Elevator - 1].Add(item);
                    }
                }

                if (Elevator < 3) // Move up
                {
                    ++upState.Elevator;
                    if(upState.IsValidState())
                    {
                        result.Add(upState);
                    }
                }

                if (Elevator > 0) // Move down
                {
                    --downState.Elevator;
                    if (downState.IsValidState())
                    {
                        result.Add(downState);
                    }
                }
            }

            return result;
        }

        public bool IsFinished()
        {
            return StateValidator.IsFinalState(this);
        }

        private bool IsValidState()
        {
            return StateValidator.ChipsAreNotFried(this) && StateValidator.IsValidElevatorMove(this);
        }
    }
}