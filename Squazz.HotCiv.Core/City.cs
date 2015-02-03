using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squazz.HotCiv
{
    class CityImpl : ICity
    {
        private readonly Player _owner;

        public CityImpl(Player owner)
        {
            _owner = owner;
        }

        public Player GetOwner() { return _owner; }

        public int GetSize() { return 1; }

        public string GetProduction() { return "archer"; }

        public string GetWorkforceFocus() { throw new NotImplementedException(); }
    }
}
