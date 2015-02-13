using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class DeltaCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game(new AgeStrategyLinear(), new WinningStrategyRedWins(), new WorldLayoutStrategyDeltaWorld());
        }

        [TestMethod]
        public void ShouldHaveRedCityAt8_12()
        {
            Position redCityPosition = new Position(8, 12);
            Assert.IsNotNull(_game.GetCityAt(redCityPosition));
            Assert.AreEqual(Player.RED, _game.GetCityAt(redCityPosition).Owner);
        }

        [TestMethod]
        public void ShouldHaveBlueCityAt4_5()
        {
            Position blueCityPosition = new Position(4, 5);
            Assert.IsNotNull(_game.GetCityAt(blueCityPosition));
            Assert.AreEqual(Player.BLUE, _game.GetCityAt(blueCityPosition).Owner);

        }

        [TestMethod]
        public void ShouldHaveRedArcherAt3_8()
        {
            Position redArcherPosition = new Position(3, 8);
            Assert.IsNotNull(_game.GetUnitAt(redArcherPosition));
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(redArcherPosition).Type);
            Assert.AreEqual(Player.RED, _game.GetUnitAt(redArcherPosition).Owner);
        }

        [TestMethod]
        public void ShouldHaveRedSettlerAt5_5()
        {
            Position redSettlerPosition = new Position(5, 5);
            Assert.IsNotNull(_game.GetUnitAt(redSettlerPosition));
            Assert.AreEqual(GameConstants.Settler, _game.GetUnitAt(redSettlerPosition).Type);
            Assert.AreEqual(Player.RED, _game.GetUnitAt(redSettlerPosition).Owner);
        }

        [TestMethod]
        public void ShouldHaveBlueLegionAt4_4()
        {
            Position blueLegionPosition = new Position(4, 4);
            Assert.IsNotNull(_game.GetUnitAt(blueLegionPosition));
            Assert.AreEqual(GameConstants.Legion, _game.GetUnitAt(blueLegionPosition).Type);
            Assert.AreEqual(Player.BLUE, _game.GetUnitAt(blueLegionPosition).Owner);
        }

        [TestMethod]
        public void WeShouldHaveOceanAtTheRightPlaces()
        {
            Assert.AreEqual(GameConstants.Ocean, _game.GetTileAt(new Position(0, 0)).Type);
            Assert.AreEqual(GameConstants.Ocean, _game.GetTileAt(new Position(0, 11)).Type);
            Assert.AreEqual(GameConstants.Ocean, _game.GetTileAt(new Position(11, 13)).Type);
            Assert.AreEqual(GameConstants.Ocean, _game.GetTileAt(new Position(15, 4)).Type);
        }

        [TestMethod]
        public void WeShouldHaveMountainsAtTheRightPlaces()
        {
            Assert.AreEqual(GameConstants.Mountains, _game.GetTileAt(new Position(0, 5)).Type);
            Assert.AreEqual(GameConstants.Mountains, _game.GetTileAt(new Position(2, 6)).Type);
            Assert.AreEqual(GameConstants.Mountains, _game.GetTileAt(new Position(7, 13)).Type);
            Assert.AreEqual(GameConstants.Mountains, _game.GetTileAt(new Position(11, 4)).Type);
        }

        [TestMethod]
        public void WeShouldHaveHillsAtTheRightPlaces()
        {
            Assert.AreEqual(GameConstants.Hills, _game.GetTileAt(new Position(1, 3)).Type);
            Assert.AreEqual(GameConstants.Hills, _game.GetTileAt(new Position(1, 4)).Type);
            Assert.AreEqual(GameConstants.Hills, _game.GetTileAt(new Position(4, 8)).Type);
            Assert.AreEqual(GameConstants.Hills, _game.GetTileAt(new Position(5, 12)).Type);
        }

        [TestMethod]
        public void WeShouldHaveForrestsAtTheRightPlaces()
        {
            Assert.AreEqual(GameConstants.Forest, _game.GetTileAt(new Position(1, 9)).Type);
            Assert.AreEqual(GameConstants.Forest, _game.GetTileAt(new Position(5, 2)).Type);
            Assert.AreEqual(GameConstants.Forest, _game.GetTileAt(new Position(9, 2)).Type);
            Assert.AreEqual(GameConstants.Forest, _game.GetTileAt(new Position(9, 11)).Type);
            Assert.AreEqual(GameConstants.Forest, _game.GetTileAt(new Position(12, 9)).Type);
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
