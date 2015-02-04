using System;

namespace Squazz.HotCiv
{
    class Tile : ITile
    {
        public String Type { get; private set; }

        public Tile(String type)
        {
            Type = type;
        }
    }
}
