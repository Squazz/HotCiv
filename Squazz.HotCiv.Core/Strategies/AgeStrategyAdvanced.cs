namespace Squazz.HotCiv.Strategies
{
    public class AgeStrategyAdvanced : IAgeStrategy
    {
        public int CalculateNewAge(int currentAge)
        {
            if (currentAge < -100) return currentAge + 100;
            if (currentAge == -100) return -1;
            if (currentAge == -1) return 1;
            if (currentAge == 1) return 50;
            if (currentAge <= 1750) return currentAge + 50;
            if (currentAge <= 1900) return currentAge + 25;
            if (currentAge <= 1970) return currentAge + 5;
            return ++currentAge;
        }
    }
}