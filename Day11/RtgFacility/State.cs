using System.Collections.Generic;
using System.Linq;

namespace RtgFacility
{
    public class State
    {
        // TODO: Implement equals method
        public IDictionary<int, Floor> Floors { get; set; }
        public int Elevator { get; set; }
        public int Moves { get; set; }

        /*
            Chips is fried if with foregin generator (regardless if foregin generator is connected to a chip)
            Bring all items to 4th floor
            Elevator has capacity of 2 items
            Elevator only moves with atleast 1 item
            Each elevator stop counts as one step, even if nothing is added to or removed from it
        */

        public IList<State> GetNextMoves()
        {
            var result = new List<State>();

            var currentFloor = Floors[Elevator];

            // Create items to move for each new move possible
            var oneItems = currentFloor.Components.Select(x => new List<Component> { x });
            var twoItems = currentFloor.Components.GetPermutations(2);
            var itemPairs = oneItems.Concat(twoItems);

            foreach(var itemPair in itemPairs)
            {
                foreach(var item in itemPair)
                {
                    if(Elevator < 4) // Move up
                    {
                        var newState = this.MemberwiseClone() as State;
                        var toRemove = newState.Floors[newState.Elevator].Components.Single(x => x.Name == item.Name && x.Type == item.Type);

                        newState.Floors[newState.Elevator].Components.Remove(toRemove);
                        ++newState.Elevator;
                        newState.Floors[newState.Elevator].Components.Add(item);

                        result.Add(newState);
                    }
                    
                    if(Elevator > 1) // Move down
                    {
                        var newState = this.MemberwiseClone() as State;
                        var toRemove = newState.Floors[newState.Elevator].Components.Single(x => x.Name == item.Name && x.Type == item.Type);

                        newState.Floors[newState.Elevator].Components.Remove(toRemove);
                        --newState.Elevator;
                        newState.Floors[newState.Elevator].Components.Add(item);

                        result.Add(newState);
                    }
                }
            }

            return result;
        }

        private bool IsValidState()
        {
            return StateValidator.ChipsAreNotFried(this) && StateValidator.IsValidElevatorMove(this);
        }

        public bool IsFinished()
        {
            return StateValidator.IsFinalState(this);
        }
    }
}

