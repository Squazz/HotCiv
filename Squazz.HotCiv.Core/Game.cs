using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class Game : IGame {

        private Player _player   = Player.RED;
        private int _age = -4000;

        private Dictionary<Position, ICity> _cities = new Dictionary<Position, ICity>();
        private Dictionary<Position, IUnit> _units = new Dictionary<Position, IUnit>();
        private Dictionary<Position, ITile> _tiles = new Dictionary<Position, ITile>();
        
        public Game()
        {
            _cities.Add(new Position(1, 1), new City(Player.RED));
            _cities.Add(new Position(4, 1), new City(Player.BLUE));

            _units.Add(new Position(2, 0), new Unit(Player.RED, "archer"));
            _units.Add(new Position(3, 2), new Unit(Player.BLUE, "legion"));
            _units.Add(new Position(4, 3), new Unit(Player.RED, "settler"));

            _tiles.Add(new Position(1, 0), new Tile(new Position(1, 0), "ocean"));
            _tiles.Add(new Position(0, 1), new Tile(new Position(0, 1), "hill"));
            _tiles.Add(new Position(2, 2), new Tile(new Position(2, 2), "mountain"));
        }

        public ITile GetTileAt(Position position)
        {
            ITile tile;
            _tiles.TryGetValue(position, out tile);
            return tile;
        }

        public IUnit GetUnitAt(Position position)
        {
            IUnit unit;
            _units.TryGetValue(position, out unit);
            return unit;
        }

        public ICity GetCityAt(Position position)
        {
            ICity city;
            _cities.TryGetValue(position, out city);
            return city;
        }

        public Player GetPlayerInTurn() { return _player; }

        public Player? GetWinner() { return null; }

        public int GetAge() { return _age; }

        public bool MoveUnit( Position from, Position to )
        {
            IUnit unit = this.GetUnitAt(from);
            _units.Remove(from);
            _units.Add(to, unit);
            return true;
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