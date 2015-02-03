using System;

namespace Squazz.HotCiv
{
    public interface ICity
    {
        /** return the owner of this city.
         * @return the player that controls this city.
         */
        Player GetOwner();

        /** return the size of the population.
         * @return population size.
         */
        int GetSize();

        /** return the type of unit this city is currently producing.
         * @return a string type defining the unit under production,
         * see GameConstants for valid values.
         */
        String GetProduction();

        /** return the work force's focus in this city.
         * @return a string type defining the focus, see GameConstants
         * for valid return values.
         */
        String GetWorkforceFocus();

    }
}
