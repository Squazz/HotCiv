using System.Collections.Generic;

namespace Squazz.HotCiv.Strategies
{
    public class WorldLayoutStrategyDeltaWorld : IWorldLayoutStrategy
    {
        public Dictionary<Position, ITile> CreateTiles()
        {
            Dictionary<Position, ITile> tiles = new Dictionary<Position, ITile>
            {
                // Add Tiles
                // Add Ocean tiles
                {new Position(0, 0), new Tile(GameConstants.Ocean)},
                {new Position(0, 1), new Tile(GameConstants.Ocean)},
                {new Position(0, 2), new Tile(GameConstants.Ocean)},
                {new Position(0, 11), new Tile(GameConstants.Ocean)},
                {new Position(0, 12), new Tile(GameConstants.Ocean)},
                {new Position(0, 13), new Tile(GameConstants.Ocean)},
                {new Position(0, 14), new Tile(GameConstants.Ocean)},
                {new Position(0, 15), new Tile(GameConstants.Ocean)},
                {new Position(1, 0), new Tile(GameConstants.Ocean)},
                {new Position(1, 2), new Tile(GameConstants.Ocean)},
                {new Position(1, 14), new Tile(GameConstants.Ocean)},
                {new Position(1, 15), new Tile(GameConstants.Ocean)},
                {new Position(2, 0), new Tile(GameConstants.Ocean)},
                {new Position(2, 10), new Tile(GameConstants.Ocean)},
                {new Position(2, 11), new Tile(GameConstants.Ocean)},
                {new Position(2, 12), new Tile(GameConstants.Ocean)},
                {new Position(2, 15), new Tile(GameConstants.Ocean)},
                {new Position(3, 0), new Tile(GameConstants.Ocean)},
                {new Position(3, 10), new Tile(GameConstants.Ocean)},
                {new Position(3, 11), new Tile(GameConstants.Ocean)},
                {new Position(4, 0), new Tile(GameConstants.Ocean)},
                {new Position(4, 1), new Tile(GameConstants.Ocean)},
                {new Position(4, 2), new Tile(GameConstants.Ocean)},
                {new Position(4, 14), new Tile(GameConstants.Ocean)},
                {new Position(4, 15), new Tile(GameConstants.Ocean)},
                {new Position(5, 0), new Tile(GameConstants.Ocean)},
                {new Position(5, 15), new Tile(GameConstants.Ocean)},
                {new Position(6, 0), new Tile(GameConstants.Ocean)},
                {new Position(6, 1), new Tile(GameConstants.Ocean)},
                {new Position(6, 2), new Tile(GameConstants.Ocean)},
                {new Position(6, 6), new Tile(GameConstants.Ocean)},
                {new Position(6, 7), new Tile(GameConstants.Ocean)},
                {new Position(6, 8), new Tile(GameConstants.Ocean)},
                {new Position(6, 9), new Tile(GameConstants.Ocean)},
                {new Position(6, 10), new Tile(GameConstants.Ocean)},
                {new Position(6, 11), new Tile(GameConstants.Ocean)},
                {new Position(6, 12), new Tile(GameConstants.Ocean)},
                {new Position(6, 13), new Tile(GameConstants.Ocean)},
                {new Position(6, 14), new Tile(GameConstants.Ocean)},
                {new Position(6, 15), new Tile(GameConstants.Ocean)},
                {new Position(7, 0), new Tile(GameConstants.Ocean)},
                {new Position(7, 6), new Tile(GameConstants.Ocean)},
                {new Position(7, 14), new Tile(GameConstants.Ocean)},
                {new Position(7, 15), new Tile(GameConstants.Ocean)},
                {new Position(8, 0), new Tile(GameConstants.Ocean)},
                {new Position(8, 6), new Tile(GameConstants.Ocean)},
                {new Position(8, 14), new Tile(GameConstants.Ocean)},
                {new Position(8, 15), new Tile(GameConstants.Ocean)},
                {new Position(9, 8), new Tile(GameConstants.Ocean)},
                {new Position(10, 8), new Tile(GameConstants.Ocean)},
                {new Position(10, 9), new Tile(GameConstants.Ocean)},
                {new Position(10, 10), new Tile(GameConstants.Ocean)},
                {new Position(11, 0), new Tile(GameConstants.Ocean)},
                {new Position(11, 10), new Tile(GameConstants.Ocean)},
                {new Position(11, 11), new Tile(GameConstants.Ocean)},
                {new Position(11, 12), new Tile(GameConstants.Ocean)},
                {new Position(11, 13), new Tile(GameConstants.Ocean)},
                {new Position(11, 14), new Tile(GameConstants.Ocean)},
                {new Position(11, 15), new Tile(GameConstants.Ocean)},
                {new Position(12, 0), new Tile(GameConstants.Ocean)},
                {new Position(12, 1), new Tile(GameConstants.Ocean)},
                {new Position(12, 14), new Tile(GameConstants.Ocean)},
                {new Position(12, 15), new Tile(GameConstants.Ocean)},
                {new Position(13, 0), new Tile(GameConstants.Ocean)},
                {new Position(13, 1), new Tile(GameConstants.Ocean)},
                {new Position(13, 2), new Tile(GameConstants.Ocean)},
                {new Position(13, 3), new Tile(GameConstants.Ocean)},
                {new Position(13, 13), new Tile(GameConstants.Ocean)},
                {new Position(13, 14), new Tile(GameConstants.Ocean)},
                {new Position(13, 15), new Tile(GameConstants.Ocean)},
                {new Position(14, 0), new Tile(GameConstants.Ocean)},
                {new Position(14, 1), new Tile(GameConstants.Ocean)},
                {new Position(14, 9), new Tile(GameConstants.Ocean)},
                {new Position(14, 10), new Tile(GameConstants.Ocean)},
                {new Position(14, 11), new Tile(GameConstants.Ocean)},
                {new Position(14, 12), new Tile(GameConstants.Ocean)},
                {new Position(14, 13), new Tile(GameConstants.Ocean)},
                {new Position(14, 14), new Tile(GameConstants.Ocean)},
                {new Position(14, 15), new Tile(GameConstants.Ocean)},
                {new Position(15, 0), new Tile(GameConstants.Ocean)},
                {new Position(15, 1), new Tile(GameConstants.Ocean)},
                {new Position(15, 2), new Tile(GameConstants.Ocean)},
                {new Position(15, 3), new Tile(GameConstants.Ocean)},
                {new Position(15, 4), new Tile(GameConstants.Ocean)},
                {new Position(15, 14), new Tile(GameConstants.Ocean)},
                {new Position(15, 15), new Tile(GameConstants.Ocean)},
                
                // Add Hill tiles
                {new Position(1, 3), new Tile(GameConstants.Hills)},
                {new Position(1, 4), new Tile(GameConstants.Hills)},
                {new Position(4, 8), new Tile(GameConstants.Hills)},
                {new Position(4, 9), new Tile(GameConstants.Hills)},
                {new Position(5, 11), new Tile(GameConstants.Hills)},
                {new Position(5, 12), new Tile(GameConstants.Hills)},
                {new Position(7, 10), new Tile(GameConstants.Hills)},
                {new Position(8, 9), new Tile(GameConstants.Hills)},
                {new Position(14, 5), new Tile(GameConstants.Hills)},
                {new Position(14, 6), new Tile(GameConstants.Hills)},
                
                // Add Mountain tiles
                {new Position(0, 5), new Tile(GameConstants.Mountains)},
                {new Position(2, 6), new Tile(GameConstants.Mountains)},
                {new Position(3, 3), new Tile(GameConstants.Mountains)},
                {new Position(3, 4), new Tile(GameConstants.Mountains)},
                {new Position(3, 5), new Tile(GameConstants.Mountains)},
                {new Position(7, 13), new Tile(GameConstants.Mountains)},
                {new Position(11, 3), new Tile(GameConstants.Mountains)},
                {new Position(11, 4), new Tile(GameConstants.Mountains)},
                {new Position(11, 5), new Tile(GameConstants.Mountains)},

                // Add Forrest tiles
                {new Position(1, 9), new Tile(GameConstants.Forest)},
                {new Position(1, 10), new Tile(GameConstants.Forest)},
                {new Position(1, 11), new Tile(GameConstants.Forest)},
                {new Position(5, 2), new Tile(GameConstants.Forest)},
                {new Position(8, 13), new Tile(GameConstants.Forest)},
                {new Position(9, 1), new Tile(GameConstants.Forest)},
                {new Position(9, 2), new Tile(GameConstants.Forest)},
                {new Position(9, 3), new Tile(GameConstants.Forest)},
                {new Position(9, 10), new Tile(GameConstants.Forest)},
                {new Position(9, 11), new Tile(GameConstants.Forest)},
                {new Position(12, 8), new Tile(GameConstants.Forest)},
                {new Position(12, 9), new Tile(GameConstants.Forest)}
            };
            return tiles;
        }

        public Dictionary<Position, ICity> CreateCities()
        {
            Dictionary<Position, ICity> cities = new Dictionary<Position, ICity>
            {
                // Add Standard Cities
                {new Position(8, 12), new City(Player.RED, new Position(8, 12))},
                {new Position(4, 5), new City(Player.BLUE, new Position(4, 5))}
            };
            return cities;
        }

        public Dictionary<Position, IUnit> CreateUnits()
        {
            Dictionary<Position, IUnit> units = new Dictionary<Position, IUnit>
            {
                // Add standard units
                {new Position(3, 8), new Unit(Player.RED, GameConstants.Archer)},
                {new Position(4, 4), new Unit(Player.BLUE, GameConstants.Legion)},
                {new Position(5, 5), new Unit(Player.RED, GameConstants.Settler)}
            };
            return units;
        }
    }
}