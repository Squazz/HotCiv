using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class AlphaCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game();
        }

        [TestMethod]
        public void ShouldStatInAge4000BC()
        {
            Assert.AreEqual(-4000, _game.GetAge(), "Age should be -4000");
        }

        [TestMethod]
        public void ShouldIncreaseAgeBy100EachRound()
        {
            Assert.AreEqual(-4000, _game.GetAge(), "Age should be -4000");
            EndRounds(1);
            Assert.AreEqual(-3900, _game.GetAge(), "Age should be -3900");
            EndRounds(2);
            Assert.AreEqual(-3700, _game.GetAge(), "Age should be -3700");
        }

        [TestMethod]
        public void RedShouldBeFirst()
        {
            Assert.AreEqual(Player.RED, _game.GetPlayerInTurn(), "RED should be the first player");
        }

        [TestMethod]
        public void BlueShouldBeSecond()
        {
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.GetPlayerInTurn(), "BLUE should be the secund player");
        }

        [TestMethod]
        public void RedShouldBeAfterBlue()
        {
            Assert.AreEqual(Player.RED, _game.GetPlayerInTurn(), "RED should be first");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.GetPlayerInTurn(), "BLUE should be second");
            _game.EndOfTurn();
            Assert.AreEqual(Player.RED, _game.GetPlayerInTurn(), "RED should be after BLUE");

        }

        [TestMethod]
        public void BlueShouldBeAfterRed()
        {
            Assert.AreEqual(Player.RED, _game.GetPlayerInTurn(), "RED should be first");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.GetPlayerInTurn(), "BLUE should be second");
            _game.EndOfTurn();
            Assert.AreEqual(Player.RED, _game.GetPlayerInTurn(), "RED should be after BLUE");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.GetPlayerInTurn(), "BLUE should be after RED");

        }

        [TestMethod]
        public void ShouldHaveCityAt1_1()
        {
            Assert.IsNotNull(_game.GetCityAt(new Position(1, 1)), "Should have a city at 1,1");
        }

        [TestMethod]
        public void ShouldHaveCityAt4_1()
        {
            Assert.IsNotNull(_game.GetCityAt(new Position(4, 1)), "Should have a city at 4,1");
        }

        [TestMethod]
        public void RedShouldOwnCityAt1_1()
        {
            ICity city = _game.GetCityAt(new Position(1, 1));
            Assert.IsNotNull(city, "Should have a city at 1,1");
            Assert.AreEqual(Player.RED, city.GetOwner(), "RED should own city at 1,1");
        }

        [TestMethod]
        public void BlueShouldOwnCityAt4_1()
        {
            ICity city = _game.GetCityAt(new Position(4, 1));
            Assert.IsNotNull(city, "Should have a city at 4,1");
            Assert.AreEqual(Player.BLUE, city.GetOwner(), "BLUE should own city at 4,1");
        }

        [TestMethod]
        public void ShouldNotHaveCityAt3_0()
        {
            Assert.IsNull(_game.GetCityAt(new Position(3,0)), "There shouldn't be a city at 3,0");
        }

        [TestMethod]
        public void SouldStartWithArcherAt2_0()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 2,0");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
        }

        [TestMethod]
        public void SouldStartWithRedArcherAt2_0()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We Should have a unit at 2,0");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
            Assert.AreEqual(Player.RED, unit.GetOwner(), "Owner should be RED");
        }

        [TestMethod]
        public void ShouldStartWithLegionAt3_2()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 3,2");
            Assert.AreEqual("legion", unit.GetTypeString(), "Type should be legion");
        }

        [TestMethod]
        public void ShouldStartWithBlueLegionAt3_2()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 3,2");
            Assert.AreEqual("legion", unit.GetTypeString(), "Type should be legion");
            Assert.AreEqual(Player.BLUE, unit.GetOwner(), "Owner should be BLUE");
        }

        [TestMethod]
        public void ShouldStartWithSettlerAt4_3()
        {
            Position position = new Position(4,3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.GetTypeString(), "Type should be settler");
        }

        [TestMethod]
        public void ShouldStartWithRedSettlerAt4_3()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.GetTypeString(), "Type should be settler");
            Assert.AreEqual(Player.RED, unit.GetOwner(), "Owner should be RED");
        }

        [TestMethod]
        public void ArchersShouldHave2Attack()
        {
            Position position = new Position(2,0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 0,0");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
            Assert.AreEqual(2, unit.GetAttackingStrength());
        }

        [TestMethod]
        public void ArchersShouldHave3Defence()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 2,0");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
            Assert.AreEqual(3, unit.GetDefensiveStrength());
        }

        [TestMethod]
        public void LegionsShouldHave4Attack()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 3,2");
            Assert.AreEqual("legion", unit.GetTypeString(), "Type should be legion");
            Assert.AreEqual(4, unit.GetAttackingStrength());
        }

        [TestMethod]
        public void LegionsShouldHave2Defence()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 3,2");
            Assert.AreEqual("legion", unit.GetTypeString(), "Type should be legion");
            Assert.AreEqual(2, unit.GetDefensiveStrength());
        }

        [TestMethod]
        public void SettlersShouldHave0Attack()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.GetTypeString(), "Type should be settler");
            Assert.AreEqual(0, unit.GetAttackingStrength());
        }

        [TestMethod]
        public void SettlersShouldHave3Defence()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.GetTypeString(), "Type should be settler");
            Assert.AreEqual(3, unit.GetDefensiveStrength());
        }

        [TestMethod]
        public void ShouldNotHaveAWinnerBefore3000BC()
        {
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 4000BC");
            EndRounds(9);
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 3100BC");
        }

        [TestMethod]
        public void CitiesShouldStartWithPopulationOf1()
        {
            ICity redCity = _game.GetCityAt(new Position(1,1));
            ICity blueCity = _game.GetCityAt(new Position(4, 1));

            Assert.AreEqual(1, redCity.GetSize(), "RED city should have a size of 1");
            Assert.AreEqual(1, blueCity.GetSize(), "BLUE city should have a size of 1");
        }

        [TestMethod]
        public void CitiesShouldAlwaysHaveaPopulationOf1()
        {
            ICity redCity = _game.GetCityAt(new Position(1, 1));
            ICity blueCity = _game.GetCityAt(new Position(4, 1));

            Assert.AreEqual(1, redCity.GetSize(), "RED city should have a size of 1");
            Assert.AreEqual(1, blueCity.GetSize(), "BLUE city should have a size of 1");
            EndRounds(10); // End the game
            Assert.AreEqual(1, redCity.GetSize(), "RED city should have a size of 1 in the endgame");
            Assert.AreEqual(1, blueCity.GetSize(), "BLUE city should have a size of 1 in the endgame");
        }

        [TestMethod]
        public void ShouldbeAbleToMoveArcherTo3_0()
        {
            Position from = new Position(2, 0);
            Position to = new Position(3, 0);
            Assert.IsTrue(_game.MoveUnit(from, to), "We should be able to move the unit");
            Assert.IsNotNull(_game.GetUnitAt(to), "Unit should be at the new position");
            Assert.IsNull(_game.GetUnitAt(from), "The unit should not be at the old position");
        }

        [TestMethod]
        public void OnlyOneUnitAtATime()
        {
            _game.MoveUnit(new Position(2, 0), new Position(3, 1));
            _game.MoveUnit(new Position(3, 1), new Position(3, 2));
        }

        // Shortcode for ending 2 turns
        public void EndRounds(int rounds = 1)
        {
            for (int i = 1; i <= rounds; i++)
            {
                _game.EndOfTurn();
                _game.EndOfTurn();
            }
        }
    }
}