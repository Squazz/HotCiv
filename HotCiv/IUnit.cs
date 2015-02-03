using System;

namespace Squazz.HotCiv
{
    public interface IUnit
    {

        /** return the type of the unit
         * @return unit type as a string, valid values are at
         * least those listed in GameConstants, particular variants
         * may define more strings to be valid.
         */
        String GetTypeString();

        /** return the owner of this unit.
         * @return the player that controls this unit.
         */
        Player GetOwner();

        /** return the move distance left (move count).
         * A move count of N means the unit may travel
         * a distance of N in this turn. The move count
         * is reset to the units maximal value before
         * a new turn starts.
         * @return the move count
         */
        int GetMoveCount();

        /** return the defensive strength of this unit
         * @return defensive strength
         */
        int GetDefensiveStrength();

        /** return the attack strength of this unit
         * @return attack strength
         */
        int GetAttackingStrength();
    }
}
