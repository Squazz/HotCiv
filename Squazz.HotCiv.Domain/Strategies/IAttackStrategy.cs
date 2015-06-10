using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public interface IAttackStrategy
    {
        IUnit Attack(
            Position attacker, 
            Position defence,
            Dictionary<Position, IUnit> units,
            Dictionary<Position, ICity> cities = null, 
            int attackerAttack = 0, 
            int defenderDefence = 0,
            Dictionary<Position, ITile> tiles = null, 
            Dictionary<Position, IUnit> fortifiedArchers = null
        );
    }
}