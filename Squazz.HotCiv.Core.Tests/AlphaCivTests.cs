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
        public void ShouldHaveCityAt1_1()
        {
            Assert.IsNotNull(_game.GetCityAt(new Position(1, 1)), "Should have a city at 1,1");
            Assert.AreEqual(Player.RED, _game.GetCityAt(new Position(1, 1)).Owner, "City at (1,1) should be owned by red");
        }
    }
}