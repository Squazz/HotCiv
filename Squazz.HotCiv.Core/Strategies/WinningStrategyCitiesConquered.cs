using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class WinningStrategyCitiesConquered : IWinningStrategy
    {
        public Player? GetWinner(int age, Dictionary<Position, ICity> cities )
        {
            Player? owner = null;
            foreach (var city in cities)
            {
                if (owner == null)
                {
                    owner = city.Value.Owner;
                }
                if (city.Value.Owner != owner)
                {
                    return null;
                }
            }
            return owner;
        }
    }
}