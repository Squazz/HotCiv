namespace Squazz.HotCiv.Strategies
{
    public class AgeStrategyLinear : IAgeStrategy
    {
        public int CalculateNewAge(int currentAge)
        {
            return currentAge + 100;
        }
    }
}