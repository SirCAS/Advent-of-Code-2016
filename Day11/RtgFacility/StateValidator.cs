using System.Linq;

namespace RtgFacility
{
    public static class StateValidator
    {
        // Elevator only moves with atleast 1 item
        public static bool IsValidElevatorMove(State state)
        {
            var currentFloor = state.Floors[state.Elevator];

            // Check if elevator is on floor without items
            if (currentFloor.Components.IsEmpty())
            {
                return false;
            }
            
            return true;
        }

        // Chips without matching generator is fried if on same level as foregin generator
        public static bool ChipsAreNotFried(State state)
        {
            foreach(var floor in state.Floors.Values)
            {
                var chips = floor.Components.Where(x => x.Type == ComponentType.Chip);
                var generators = floor.Components.Where(x => x.Type == ComponentType.Generator);

                // Chips can only be fried if there is both chips and generators
                if(chips.Any() && generators.Any())
                {
                    // Every chip must have a shield when there is an generator
                    if(chips.Any(c => !generators.Any(g => g.Name == c.Name)))
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
            if(state.Elevator != 4)
            {
                return false;
            }

            // There can only be items at assembly floor
            var nonAssemblyFloors = state.Floors.Where(x => x.Key != 4);
            return nonAssemblyFloors.All(x => x.Value.Components.IsEmpty());
        }
    }
}

