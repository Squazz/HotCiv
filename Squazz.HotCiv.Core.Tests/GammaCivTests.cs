using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class GammaCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game(new AgeStrategyLinear(), new WinningStrategyRedWins(), new WorldLayoutStrategyAlphaWorld(), new AttackStrategyAttackerWins(), new ActionStrategyDoAction());
        }

        [TestMethod]
        public void ShouldBeAbleToPerformArcherAction()
        {
            Position archerPosition = new Position(2,0);
            _game.PerformUnitActionAt(archerPosition);
            Assert.AreEqual(6, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(0, _game.GetUnitAt(archerPosition).Moves);
            EndRounds();
            Assert.AreEqual(6, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(0, _game.GetUnitAt(archerPosition).Moves);
        }

        [TestMethod]
        public void PerformingArcherAction2TimesShouldResetTheFortification()
        {
            Position archerPosition = new Position(2, 0);
            _game.PerformUnitActionAt(archerPosition);
            Assert.AreEqual(6, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(0, _game.GetUnitAt(archerPosition).Moves);
            EndRounds();
            Assert.AreEqual(6, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(0, _game.GetUnitAt(archerPosition).Moves);
            _game.PerformUnitActionAt(archerPosition);
            Assert.AreEqual(3, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(0, _game.GetUnitAt(archerPosition).Moves);
            EndRounds();
            Assert.AreEqual(3, _game.GetUnitAt(archerPosition).Defense);
            Assert.AreEqual(1, _game.GetUnitAt(archerPosition).Moves);
        }

        [TestMethod]
        public void ShouldBeAbleToPerformSettlerAction()
        {
            Position settlerPosition = new Position(4, 3);
            Assert.IsNotNull(_game.GetUnitAt(settlerPosition));
            _game.PerformUnitActionAt(settlerPosition);
            Assert.IsNull(_game.GetUnitAt(settlerPosition));
            Assert.IsNotNull(_game.GetCityAt(settlerPosition));
            Assert.AreEqual(Player.RED, _game.GetCityAt(settlerPosition).Owner);
        }

        // Shortcode for ending 2 turns
        private void EndRounds(int rounds = 1)
        {
            for (int i = 1; i <= rounds; i++)
            {
                _game.EndOfTurn();
                _game.EndOfTurn();
            }
        }
    }
}
