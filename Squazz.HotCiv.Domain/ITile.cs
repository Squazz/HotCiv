using System;

namespace Squazz.HotCiv
{
    public interface ITile
    {
        /** return position of this tile on the board. 
         * @return position of tile.
         */
        Position Position { get; }

        /** return the tile type as a string. The set of
         * valid strings are defined by the graphics
         * engine, as they correspond to named image files.
         * @return the type type as string
         */
        String Type { get; }
    }
}
