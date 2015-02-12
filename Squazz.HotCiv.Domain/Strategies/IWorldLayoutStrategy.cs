
using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public interface IWorldLayoutStrategy
    {
        Dictionary<Position, ITile> CreateTiles();
        Dictionary<Position, ICity> CreateCities();
        Dictionary<Position, IUnit> CreateUnits();
    }
}
