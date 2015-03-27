using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class AgingLinearTests
    {
        [TestMethod]
        public void Parameter4000BCShouldResultIn3900BC()
        {
            IAgeStrategy ageStrategy = new AgeStrategyLinear();
            Assert.AreEqual(-3900, ageStrategy.CalculateNewAge(-4000), "Calculated age should be -3900");
        }

        [TestMethod]
        public void Parameter3000BCShouldResultIn2900BC()
        {
            IAgeStrategy ageStrategy = new AgeStrategyLinear();
            Assert.AreEqual(-2900, ageStrategy.CalculateNewAge(-3000), "Calculated age should be -2900");
        }

        [TestMethod]
        public void Parameter1500BCShouldResultIn1400BC()
        {
            IAgeStrategy ageStrategy = new AgeStrategyLinear();
            Assert.AreEqual(-2900, ageStrategy.CalculateNewAge(-3000), "Calculated age should be -1400");
        }
    }
}
