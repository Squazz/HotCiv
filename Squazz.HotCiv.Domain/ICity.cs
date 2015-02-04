using System;

namespace Squazz.HotCiv
{
    public interface ICity
    {
        /** return the owner of this city.
         * @return the player that controls this city.
         */
        Player Owner { get; }

        /** return the size of the population.
         * @return population size.
         */
        int Size { get; }

        /** return the type of unit this city is currently producing.
         * @return a string type defining the unit under production,
         * see GameConstants for valid values.
         */
        String Production { get; }

        /** return the work force's focus in this city.
         * @return a string type defining the focus, see GameConstants
         * for valid return values.
         */
        String WorkforceFocus { get; }

    }
}
