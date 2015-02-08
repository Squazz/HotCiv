using System;

namespace Squazz.HotCiv
{
    class City : ICity
    {
        public Player Owner { get; private set; }
        public int Size { get; private set; }
        public String Production { get; set; }
        public int Vault { get; set; }
        public String WorkforceFocus { get; private set; }
        public Position Position { get; private set; }

        public City(Player owner, Position position)
        {
            Vault = 0;
            Size = 1;
            Owner = owner;
            Position = position;
            WorkforceFocus = null;
        }
    }
}
