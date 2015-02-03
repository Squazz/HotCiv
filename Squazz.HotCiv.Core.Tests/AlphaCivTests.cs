using System;
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
            Assert.AreEqual(4000, _game.GetAge(), "Age should be 4000");
        }

        [TestMethod]
        public void ShouldIncreaseAgeBy100EachRound()
        {
            Assert.AreEqual(4000, _game.GetAge(), "Age should be 4000");
            _game.EndRounds(1);
            Assert.AreEqual(3900, _game.GetAge(), "Age should be 3900");
            _game.EndRounds(2);
            Assert.AreEqual(3700, _game.GetAge(), "Age should be 3700");
        }

        [TestMethod]
        public void SouldStartWithArcherAt0_2()
        {
            Position position = new Position(0, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit);
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
        }
        [TestMethod]
        public void ArcherAt2_0ShouldBeOwnedByRed()
        {
            Position position = new Position(0, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit);
            Assert.AreEqual("archer", unit.GetTypeString(), "Type should be archer");
            Assert.AreEqual(Player.RED, unit.GetOwner(), "Owner should be RED");
        }
    }
}