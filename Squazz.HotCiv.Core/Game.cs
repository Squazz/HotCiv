using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class Game : IGame {

        private Player _player   = Player.RED;
        private int _age = -4000;

        private Dictionary<Position, City> _cities = new Dictionary<Position, City>();
        private Dictionary<Position, Unit> _units = new Dictionary<Position, Unit>();
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();
        
        public Game()
        {
            _cities.Add(new Position(1, 1), new City(Player.RED));
            _cities.Add(new Position(4, 1), new City(Player.BLUE));

            _units.Add(new Position(0, 2), new Unit(Player.RED, "archer"));
            _units.Add(new Position(3, 2), new Unit(Player.BLUE, "legion"));

            _tiles.Add(new Position(1, 0), new Tile(new Position(1, 0), "ocean"));
            _tiles.Add(new Position(0, 1), new Tile(new Position(0, 1), "hill"));
            _tiles.Add(new Position(2, 2), new Tile(new Position(2, 2), "mountain"));
        }

        public ITile GetTileAt(Position position)
        {
            Tile tile;
            _tiles.TryGetValue(position, out tile);
            return tile;
        }

        public IUnit GetUnitAt(Position position)
        {
            Unit unit;
            _units.TryGetValue(position, out unit);
            return unit;
        }

        public ICity GetCityAt(Position position)
        {
            City city;
            _cities.TryGetValue(position, out city);
            return city;
        }

        public Player GetPlayerInTurn() { return _player; }

        public Player? GetWinner() { return null; }

        public int GetAge() { return _age; }

        public bool MoveUnit( Position from, Position to ) 
        {
            return false;
        }

        public void EndOfTurn()
        {
            switch (_player)
            {
                case Player.RED:
                    _player = Player.BLUE;
                    break;
                case Player.BLUE:
                    _player = Player.RED;
                    _age = _age + 100;
                    break;
            }
        }
        
        public void ChangeWorkForceFocusInCityAt( Position position, String balance ) {}

        public void ChangeProductionInCityAt( Position position, String unitType ) {}

        public void PerformUnitActionAt( Position position ) {}

    }
}