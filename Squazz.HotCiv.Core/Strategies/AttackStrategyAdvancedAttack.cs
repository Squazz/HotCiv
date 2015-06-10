using System;
using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class AttackStrategyAdvancedAttack : IAttackStrategy
    {
        public IUnit Attack(Position attackerPosition, Position defencePosition,
            Dictionary<Position, IUnit> units, Dictionary<Position, ICity> cities, int attackerAttack, int defenderDefence,
            Dictionary<Position, ITile> tiles, Dictionary<Position, IUnit> fortifiedArchers)
        {
            IUnit attacker;
            IUnit defender;
            units.TryGetValue(attackerPosition, out attacker);
            units.TryGetValue(defencePosition, out defender);

            if (attacker == null) throw new ArgumentNullException("attacker");
            if (defender == null) throw new ArgumentNullException("defender");

            return attackerAttack >= defenderDefence ? attacker : defender;
        }
    }
}