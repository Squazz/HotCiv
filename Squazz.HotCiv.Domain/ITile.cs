using System;

namespace Squazz.HotCiv
{
    public interface ITile
    {
        /** return the tile type as a string. The set of
         * valid strings are defined by the graphics
         * engine, as they correspond to named image files.
         * @return the type type as string
         */
        String Type { get; }
    }
}
