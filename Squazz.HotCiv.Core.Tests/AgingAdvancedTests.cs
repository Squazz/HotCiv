using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class AgingAdvancedTests
    {
        [TestMethod]
        public void Parameter4000BCShouldResultIn3900BC()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(-3900, ageStrategy.CalculateNewAge(-4000), "Calculated age should be -3900");
        }

        [TestMethod]
        public void Parameter100BCShouldResultIn1BC()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(-1, ageStrategy.CalculateNewAge(-100), "Calculated age should be -1");
        }

        [TestMethod]
        public void Parameter1BCShouldResultIn1AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(1, ageStrategy.CalculateNewAge(-1), "Calculated age should be 1");
        }

        [TestMethod]
        public void Parameter1ADShouldResultIn50AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(50, ageStrategy.CalculateNewAge(1), "Calculated age should be 50");
        }

        [TestMethod]
        public void Parameter50ADShouldResultIn100AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(100, ageStrategy.CalculateNewAge(50), "Calculated age should be 100");
        }

        [TestMethod]
        public void Parameter1800ADShouldResultIn1825AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(1825, ageStrategy.CalculateNewAge(1800), "Calculated age should be 1825");
        }

        [TestMethod]
        public void Parameter1950ADShouldResultIn1955AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(1955, ageStrategy.CalculateNewAge(1950), "Calculated age should be 1955");
        }

        [TestMethod]
        public void Parameter1975ADShouldResultIn1976AD()
        {
            IAgeStrategy ageStrategy = new AgeStrategyAdvanced();
            Assert.AreEqual(1976, ageStrategy.CalculateNewAge(1975), "Calculated age should be 1976");
        }
    }
}
