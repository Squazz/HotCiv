using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public interface IWinningStrategy
    {
        Player? GetWinner(int age, Dictionary<Position, ICity> cities = null);
    }
}