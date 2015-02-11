namespace Squazz.HotCiv.Strategies
{
    public class WinningStrategyRedWins : IWinningStrategy
    {
        public Player? GetWinner(int age)
        {
            if(age >= -3000)
                return Player.RED;
            return null;
        }
    }
}