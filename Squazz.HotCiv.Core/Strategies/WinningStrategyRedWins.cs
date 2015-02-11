using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class WinningStrategyRedWins : IWinningStrategy
    {
        public Player? GetWinner(int age, Dictionary<Position, ICity> cities = null)
        {
            if (age >= -3000)
                return Player.RED;
            return null;
        }
    }
}