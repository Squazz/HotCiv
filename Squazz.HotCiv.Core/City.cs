using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squazz.HotCiv
{
    class City : ICity
    {
        private readonly Player _owner;

        public City(Player owner)
        {
            _owner = owner;
        }

        public Player GetOwner() { return _owner; }

        public int GetSize() { return 1; }

        public string GetProduction() { return "archer"; }

        public string GetWorkforceFocus() { throw new NotImplementedException(); }
    }
}
