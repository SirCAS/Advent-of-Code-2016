using System.Collections.Generic;
using System.Linq;

namespace RtgFacility
{
    public static class StateValidator
    {
        public static bool IsFinalState(State state)
        {
            var assemblyFloor = RtgFacilityConfiguration.Floors - 1;
            // Elevator is at 4th assembly floor
            if(state.Elevator != assemblyFloor)
            {
                return false;
            }

            // There can only be items at assembly floor
            for(int x=0; x < assemblyFloor; x++)
            {
                if(state.Components[x].Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}