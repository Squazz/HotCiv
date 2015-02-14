using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public interface IAttackStrategy
    {
        IUnit Attack(Position attacker, Position defence, Dictionary<Position, ICity> cities, Dictionary<Position, IUnit> units, Dictionary<Position, ITile> tiles = null, Dictionary<Position, IUnit> fortifiedArchers = null);
    }
}