using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squazz.HotCiv
{
    class Tile : ITile
    {
        private readonly Position _position;
        private readonly String _type;

        public Tile(Position position, String type)
        {
            _position   = position;
            _type       = type;
        }

        public string GetTypeString() { return _type; }
        public Position GetPosition() { return _position; }
    }
}
