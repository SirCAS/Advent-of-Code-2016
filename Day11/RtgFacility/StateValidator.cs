using System.Linq;

namespace RtgFacility
{
    public static class StateValidator
    {
        // Elevator only moves with atleast 1 item
        public static bool IsValidElevatorMove(State state)
        {
            var currentFloor = state.Components[state.Elevator];

            // Check if elevator is on floor without items
            if (currentFloor.IsEmpty())
            {
                return false;
            }
            
            return true;
        }

        // Chips without matching generator is fried if on same level as foregin generator
        public static bool ChipsAreNotFried(State state)
        {
            foreach(var components in state.Components.Values)
            {
                var chips = components.Where(x => x.StartsWith('C')).Select(x => x.Substring(1));
                var generators = components.Where(x => x.StartsWith('G')).Select(x => x.Substring(1));

                // Chips can only be fried if there is both chips and generators
                if(chips.Any() && generators.Any())
                {
                    // Every chip must have a shield when there is an generator
                    if(chips.Any(c => !generators.Any(g => g == c)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsFinalState(State state)
        {
            // Elevator is at 4th assembly floor
            if(state.Elevator != 3)
            {
                return false;
            }

            // There can only be items at assembly floor
            var nonAssemblyFloors = state.Components.Where(x => x.Key != 3);
            var result = nonAssemblyFloors.All(x => x.Value.IsEmpty());
            return result;
        }
    }
}

