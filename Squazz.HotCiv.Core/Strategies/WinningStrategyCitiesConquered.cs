using System.Collections.Generic;
using System.Linq;

namespace Squazz.HotCiv.Strategies
{
    public class WinningStrategyCitiesConquered : IWinningStrategy
    {
        public Player? GetWinner(int age, Dictionary<Position, ICity> cities )
        {
            Player? owner = null;
            cities.Values.All( city => { if(owner == null) {owner = city.Owner; } return city.Owner == owner; });
            return owner;
        }
    }
}