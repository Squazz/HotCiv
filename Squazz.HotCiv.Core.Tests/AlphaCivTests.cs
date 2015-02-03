using System;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class AlphaCivTests
    {
        private GameImpl _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new GameImpl();
        }

        [TestMethod]
        public void ShouldStatInAge4000()
        {
            Assert.AreEqual(-4000, _game.GetAge(), "Age should be -4000");
        }

        [TestMethod]
        public void ShouldIncreaseAgeBy100EachRound()
        {
            Assert.AreEqual(-4000, _game.GetAge(), "Age should be -4000");
            _game.EndRounds(1);
            Assert.AreEqual(-3900, _game.GetAge(), "Age should be -3900");
            _game.EndRounds(2);
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
        public void BlueShouldOwnCityAt1_1()
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
        public void SouldStartWithArcherAt0_2()
        {
            Position position = new Position(0, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 0,2");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
        }

        [TestMethod]
        public void ArcherAt2_0ShouldBeOwnedByRed()
        {
            Position position = new Position(0, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We Should have a unit at 0,2");
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
            Assert.AreEqual(Player.RED, unit.GetOwner(), "Owner should be RED");
        }

        [TestMethod]
        public void ShouldNotHaveAWinnerBefore3000BC()
        {
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 4000BC");
            _game.EndRounds(9);
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 3100BC");
        }
    }
}