using System;
using System.Security.Cryptography;

namespace Squazz.HotCiv
{
    class City : ICity
    {
        public Player Owner { get; private set; }
        public int Size { get; private set; }
        public String Production { get; private set; }
        public int Vault { get; set; }
        public String WorkforceFocus { get; private set; }

        public City(Player owner)
        {
            Vault = 0;
            Production = GameConstants.Archer;
            Size = 1;
            Owner = owner;
        }
    }
}
