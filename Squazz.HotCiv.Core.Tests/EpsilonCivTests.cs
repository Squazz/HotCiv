using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class EpsilonCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game(new AgeStrategyLinear(), new WinningStrategyRedWins(), new WorldLayoutStrategyAlphaWorld(), new AttackStrategyAdvancedAttack());
        }

        [TestMethod]
        public void StandingInACityShouldTrippleTheAttackAndDefence()
        {
            _game.MoveUnit(new Position(2, 0), new Position(1, 1));
            Assert.AreEqual(6, _game.GetActualUnitAttack(new Position(1, 1)));
            Assert.AreEqual(9, _game.GetActualUnitDefence(new Position(1, 1)));
        }

        [TestMethod]
        public void StandingOnAHillShouldDoubleTheAttackAndDefence()
        {
            _game.MoveUnit(new Position(2, 0), new Position(1, 1));
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.MoveUnit(new Position(1, 1), new Position(0, 1));
            Assert.AreEqual(4, _game.GetActualUnitAttack(new Position(0, 1)));
            Assert.AreEqual(6, _game.GetActualUnitDefence(new Position(0, 1)));
        }
    }
}
