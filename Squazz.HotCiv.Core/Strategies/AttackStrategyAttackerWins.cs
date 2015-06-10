using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class AttackStrategyAttackerWins : IAttackStrategy
    {
        public IUnit Attack(Position attacker, Position defence, 
            Dictionary<Position, IUnit> units, Dictionary<Position, ICity> cities, int attackerAttack, int defenderDefence,
            Dictionary<Position, ITile> tiles,  Dictionary<Position, IUnit> fortifiedArchers)
        {
            IUnit unit;
            var result = units.TryGetValue(attacker, out unit);

            if (result)
                units.Remove(defence);
            return unit;
        }
    }
}
