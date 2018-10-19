using System.Collections.Generic;
using System.Linq;

namespace RtgFacility
{
    public class StateGenerator
    {
        private bool IsValidState(State state)
        {
            return state != null && StateValidator.ChipsAreNotFried(state) && StateValidator.IsValidElevatorMove(state);
        }

        public HashSet<State> Next(State state)
        {
            var result = new HashSet<State>(new StateComparer());

            var elevatorFloor = state.Components[state.Elevator];
            
            var oneComponents = elevatorFloor.GetPermutations(1);
            var twoComponents = elevatorFloor.GetPermutations(2);
            var componentCombinations = oneComponents.Concat(twoComponents);

            foreach (var componentCombination in componentCombinations)
            {
                State upperFloor = null, lowerFloor = null;

                if (state.Elevator < 3)
                {
                    upperFloor = state.DeepCopy();
                    upperFloor.Moves++;
                    upperFloor.Elevator = state.Elevator + 1;
                }

                if (state.Elevator > 0)
                {
                    lowerFloor = state.DeepCopy();
                    lowerFloor.Moves++;
                    lowerFloor.Elevator = state.Elevator - 1;
                }

                foreach (var component in componentCombination)
                {
                    if (upperFloor != null)
                    {
                        upperFloor.Components[state.Elevator].Remove(component);
                        upperFloor.Components[upperFloor.Elevator].Add(component);
                    }

                    if (lowerFloor != null)
                    {
                        lowerFloor.Components[state.Elevator].Remove(component);
                        lowerFloor.Components[lowerFloor.Elevator].Add(component);
                    }
                }

                if (IsValidState(upperFloor))
                {
                    result.Add(upperFloor);
                }

                if (IsValidState(lowerFloor))
                {
                    result.Add(lowerFloor);
                }
            }

            return result;
        }
    }
}