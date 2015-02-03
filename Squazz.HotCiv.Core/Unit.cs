using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squazz.HotCiv
{
    class Unit : IUnit
    {
        private readonly Player _owner;
        private readonly String _type;
        private readonly int _attack;
        private readonly int _defence;
        private int _moves = 1;

        public Unit(Player owner, String type)
        {
            _owner  = owner;
            _type   = type;
            if (type == "archer")
            {
                _attack = 2;
                _defence = 3;
            }
            else if (type == "legion")
            {
                _attack = 4;
                _defence = 2;
            }
        }

        public string GetTypeString() { return _type; }

        public Player GetOwner() { return _owner; }

        public int GetMoveCount() { return _moves; }

        public int GetDefensiveStrength() {  return _defence; }

        public int GetAttackingStrength() { return _attack; }
    }
}