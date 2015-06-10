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

        [TestMethod]
        public void TwoAdjecentFriendlyUnitsGives2ExtraAttackAndDefence()
        {
            var redCity = new Position(1, 1);
            _game.MoveUnit(new Position(2, 0), redCity);
            Assert.AreEqual(6, _game.GetActualUnitAttack(redCity), "Attack should now be 6");
            Assert.AreEqual(9, _game.GetActualUnitDefence(redCity), "Defence should now be 9");
            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn(); // End of 3rd round
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 1)), "We should now have produced a unit");
            Assert.AreEqual(9 , _game.GetActualUnitAttack(redCity), "Attack should now be 9"); // (2+1)*3
            Assert.AreEqual(12, _game.GetActualUnitDefence(redCity), "Defence should now be 12"); // (3+1)*3

            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 2)), "We should now have produced a unit");
            Assert.AreEqual(12, _game.GetActualUnitAttack(redCity), "Attack should now be 12"); // (2+1+1)*3
            Assert.AreEqual(15, _game.GetActualUnitDefence(redCity), "Defence should now be 15"); // (3+1+1)*3
        }

        [TestMethod]
        public void AttackingUnbuffedShouldResultInLoose()
        {
            var redCity = new Position(1, 1);
            _game.MoveUnit(new Position(2, 0), redCity);
            Assert.AreEqual(6, _game.GetActualUnitAttack(redCity), "Attack should now be 6");
            Assert.AreEqual(9, _game.GetActualUnitDefence(redCity), "Defence should now be 9");
            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 2), new Position(2, 3)), "Should be able to move the blue legion");
            _game.EndOfTurn();
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(2, 3), new Position(1, 2)), "Should be able to move the blue legion");
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn(); // End of 3rd round
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 1)), "We should now have produced a unit");
            Assert.AreEqual(9, _game.GetActualUnitAttack(redCity), "Attack should now be 9"); // (2+1)*3
            Assert.AreEqual(12, _game.GetActualUnitDefence(redCity), "Defence should now be 12"); // (3+1)*3

            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 2)), "We should now have produced a unit");
            Assert.AreEqual(12, _game.GetActualUnitAttack(redCity), "Attack should now be 12"); // (2+1+1)*3
            Assert.AreEqual(15, _game.GetActualUnitDefence(redCity), "Defence should now be 15"); // (3+1+1)*3
            _game.EndOfTurn();
            Assert.AreEqual(4, _game.GetUnitAt(new Position(1, 2)).Attack, "Legion should have an attack of 4");
            Assert.IsTrue(_game.MoveUnit(new Position(1, 2), redCity), "Should be able to move the blue legion");
            Assert.AreEqual(Player.RED, _game.GetUnitAt(redCity).Owner, "The standing unit should be red");
        }

        [TestMethod]
        public void AttackingBuffedShouldResultInWin()
        {
            var redCity = new Position(1, 1);
            _game.MoveUnit(new Position(2, 0), redCity);
            Assert.AreEqual(6, _game.GetActualUnitAttack(redCity), "Attack should now be 6");
            Assert.AreEqual(9, _game.GetActualUnitDefence(redCity), "Defence should now be 9");
            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 2), new Position(2, 3)), "Should be able to move the blue legion");
            _game.EndOfTurn();
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(2, 3), new Position(1, 2)), "Should be able to move the blue legion");
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn(); // End of 3rd round
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 1)), "We should now have produced a unit");
            Assert.AreEqual(9, _game.GetActualUnitAttack(redCity), "Attack should now be 9"); // (2+1)*3
            Assert.AreEqual(12, _game.GetActualUnitDefence(redCity), "Defence should now be 12"); // (3+1)*3

            _game.ChangeProductionInCityAt(redCity, GameConstants.Archer);
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            _game.EndOfTurn();
            Assert.IsNotNull(_game.GetUnitAt(new Position(0, 2)), "We should now have produced a unit");
            Assert.AreEqual(12, _game.GetActualUnitAttack(redCity), "Attack should now be 12"); // (2+1+1)*3
            Assert.AreEqual(15, _game.GetActualUnitDefence(redCity), "Defence should now be 15"); // (3+1+1)*3
            Assert.IsTrue(_game.MoveUnit(new Position(0, 2), new Position(1, 2)), "Should be able to move the red archer");
            Assert.AreEqual(Player.RED, _game.GetUnitAt(new Position(1, 2)).Owner, "The standing unit should be red");
        }

        [TestMethod]
        public void ThreeWinsShouldResultInWinningTheGame()
        {
            Assert.IsTrue(1 == 2);
        }
    }
}