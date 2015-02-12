using System;
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
            _game = new Game(new AgeStrategyLinear(), new WinningStrategyRedWins(), new WorldLayoutStrategyAlphaWorld());
        }

        [TestMethod]
        public void TestMethod1()
        {

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
