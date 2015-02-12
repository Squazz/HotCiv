using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class WorldLayoutStrategyAlphaWorld : IWorldLayoutStrategy
    {
        public Dictionary<Position, ITile> CreateTiles()
        {
            Dictionary<Position, ITile> tiles = new Dictionary<Position, ITile>
            {
                // Add Standard Cities
                {new Position(1, 0), new Tile(GameConstants.Ocean)},
                {new Position(0, 1), new Tile(GameConstants.Hills)},
                {new Position(2, 2), new Tile(GameConstants.Mountains)}
            };
            return tiles;
        }

        public Dictionary<Position, ICity> CreateCities()
        {
            Dictionary<Position, ICity> cities = new Dictionary<Position, ICity>
            {
                // Add Standard Cities
                {new Position(1, 1), new City(Player.RED, new Position(1, 1))},
                {new Position(4, 1), new City(Player.BLUE, new Position(4, 1))}
            };
            return cities;
        }

        public Dictionary<Position, IUnit> CreateUnits()
        {
            Dictionary<Position, IUnit> units = new Dictionary<Position, IUnit>
            {
                // Add standard units
                {new Position(2, 0), new Unit(Player.RED, GameConstants.Archer)},
                {new Position(3, 2), new Unit(Player.BLUE, GameConstants.Legion)},
                {new Position(4, 3), new Unit(Player.RED, GameConstants.Settler)}
            };
            return units;
        }
    }
}