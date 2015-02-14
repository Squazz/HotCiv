using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class BetaCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game(new AgeStrategyAdvanced(), new WinningStrategyCitiesConquered(), new WorldLayoutStrategyAlphaWorld(), new AttackStrategyAttackerWins());
        }

        [TestMethod]
        public void Age4000BCShouldAdvanceTo3900BC()
        {
            Assert.AreEqual(-4000,_game.Age);
            EndRounds();
            Assert.AreEqual(-3900, _game.Age);
        }

        [TestMethod]
        public void Advancing39RoundsShouldResultIn100BC()
        {
            Assert.AreEqual(-4000, _game.Age);
            EndRounds(39);
            Assert.AreEqual(-100, _game.Age);
        }

        [TestMethod]
        public void Advancing43RoundsShouldResultIn100AD()
        {
            Assert.AreEqual(-4000, _game.Age);
            EndRounds(43);
            Assert.AreEqual(100, _game.Age);
        }

        [TestMethod]
        public void Advancing65RoundsShouldResultIn1200AD()
        {
            Assert.AreEqual(-4000, _game.Age);
            EndRounds(65);
            Assert.AreEqual(1200, _game.Age);
        }

        [TestMethod]
        public void Advancing125RoundsShouldResultIn2008AD()
        {
            Assert.AreEqual(-4000, _game.Age);
            EndRounds(125);
            Assert.AreEqual(2008, _game.Age);
        }

        [TestMethod]
        public void ShouldTakeOverCityWhenAttackingIt()
        {
            _game.MoveUnit(new Position(2, 0), new Position(3, 1));
            EndRounds();
            _game.MoveUnit(new Position(3, 1), new Position(4, 1));
            Assert.AreEqual(Player.RED, _game.GetUnitAt(new Position(4, 1)).Owner);
            Assert.AreEqual(Player.RED,_game.GetCityAt(new Position(4,1)).Owner);
        }

        [TestMethod]
        public void RedShouldWinAfterCapturingBluesCity()
        {
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner now");
            _game.MoveUnit(new Position(2, 0), new Position(3, 1));
            EndRounds();
            _game.MoveUnit(new Position(3, 1), new Position(4, 1));
            Assert.AreEqual(Player.RED, _game.GetUnitAt(new Position(4, 1)).Owner);
            Assert.AreEqual(Player.RED, _game.GetCityAt(new Position(4, 1)).Owner);
            EndRounds();
            Assert.AreEqual(Player.RED, _game.GetWinner(), "RED should now be our winner");
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
