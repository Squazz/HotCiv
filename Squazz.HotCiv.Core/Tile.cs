using System;

namespace Squazz.HotCiv
{
    class Tile : ITile
    {
        private readonly Position _position;
        public String Type { get; private set; }

        public Tile(Position position, String type)
        {
            _position   = position;
            Type = type;
        }
        
        public Position GetPosition() { return _position; }
    }
}
