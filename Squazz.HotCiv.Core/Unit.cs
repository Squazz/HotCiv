using System;

namespace Squazz.HotCiv
{
    class Unit : IUnit
    {
        public Player Owner { get; private set; }
        public String Type { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Moves { get; set; }

        public Unit(Player owner, String type)
        {
            Owner = owner;
            Type = type;
            Moves = 1;
            switch (type)
            {
                case "archer":
                    Attack = 2;
                    Defense = 3;
                    break;
                case "legion":
                    Attack = 4;
                    Defense = 2;
                    break;
                case "settler":
                    Attack = 0;
                    Defense = 3;
                    break;
            }
        }
    }
}