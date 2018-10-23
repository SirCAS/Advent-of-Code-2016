using System;
using System.Collections.Generic;
using System.Linq;

namespace RtgFacility
{
    public class StateGenerator
    {
        private readonly int[] DeltaFloors = new int[] { 1, -1 };

        public HashSet<State> Next(State state)
        {
            var movableComponents = state.Components[state.Elevator];

            var componentMutations =
                Enumerable
                    .Range(1, RtgFacilityConfiguration.ElevatorCapacity)
                    .SelectMany(mutationCount => movableComponents.GetPermutations(mutationCount));

            var nextPossibleStates =
                componentMutations
                    .SelectMany(mutation =>
                        DeltaFloors
                            .Where(deltaFloor => ElevatorIsWithinBoundaries(state.Elevator, deltaFloor))
                            .Select(deltaFloor => CreateMutatedState(state, mutation, deltaFloor))
                            .Where(s => s.HasValue)
                            .Select(s => s.Value))
                    .ToHashSet(new StateComparer());

            return nextPossibleStates;
        }

        private bool ElevatorIsWithinBoundaries(int elevatorPos, int deltaFloor)
        {
            var newFloor = deltaFloor + elevatorPos;
            return 0 <= newFloor && newFloor < RtgFacilityConfiguration.Floors;
        }

        private State? CreateMutatedState(State state, IEnumerable<string> componentCombination, int deltaFloor)
        {
            var components = new Dictionary<int, HashSet<string>>();

            for(var floor=0; floor < RtgFacilityConfiguration.Floors; ++floor)
            {
                HashSet<string> cmp;
                if (floor == state.Elevator + deltaFloor)
                {
                    cmp = state.Components[floor].Concat(componentCombination).ToHashSet();
                }
                else if (floor == state.Elevator)
                {
                    cmp = state.Components[floor].Except(componentCombination).ToHashSet();
                }
                else
                {
                    cmp = state.Components[floor];
                }
                
                // Early exit if floor has an invalid item combination
                if (ChipsAreFried(cmp))
                {
                    return null;
                }

                components.Add(floor, cmp);
            }

            return new State(state.Elevator + deltaFloor, state.Moves + 1, components);
        }

        private bool ChipsAreFried(HashSet<string> components)
        {
            // 'C' = chips, 'G' = generators
            var comp = components.ToLookup(x => x[0], x => x.Substring(1));
            
            // Chips can only be fried if there is both chips and generators
            if(comp.Count == 2)
            {
                // At this point we know there is chips, thus we should just find all chips without shield
                return comp['C'].Except(comp['G']).Any();
            }

            return false;
        }
    }
}