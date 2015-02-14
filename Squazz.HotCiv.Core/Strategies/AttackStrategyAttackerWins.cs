using System;
using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class AttackStrategyAttackerWins : IAttackStrategy
    {
        public IUnit Attack(Position attacker, Position defence, 
            Dictionary<Position, ICity> cities, Dictionary<Position, IUnit> units, 
            Dictionary<Position, ITile> tiles = null,  Dictionary<Position, IUnit> fortifiedArchers = null)
        {
            IUnit unit;
            var result = units.TryGetValue(attacker, out unit);

            if (result)
                units.Remove(defence);
            return unit;
        }
    }
}
