using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }

        private Dictionary<Position, ICity> _cities = new Dictionary<Position, ICity>();
        private Dictionary<Position, IUnit> _units = new Dictionary<Position, IUnit>();
        private Dictionary<Position, ITile> _tiles = new Dictionary<Position, ITile>();
        
        public Game()
        {
            PlayerInTurn = Player.RED;
            Age = -4000;

            _cities.Add(new Position(1, 1), new City(Player.RED));
            _cities.Add(new Position(4, 1), new City(Player.BLUE));

            _units.Add(new Position(2, 0), new Unit(Player.RED, GameConstants.Archer));
            _units.Add(new Position(3, 2), new Unit(Player.BLUE, GameConstants.Legion));
            _units.Add(new Position(4, 3), new Unit(Player.RED, GameConstants.Settler));

            _tiles.Add(new Position(1, 0), new Tile(new Position(1, 0), GameConstants.Ocean));
            _tiles.Add(new Position(0, 1), new Tile(new Position(0, 1), GameConstants.Hills));
            _tiles.Add(new Position(2, 2), new Tile(new Position(2, 2), GameConstants.Mountains));
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
        
        public Player? GetWinner()
        {
            if (Age == -3000)
            {
                return Player.RED;
            }
            return null;
        }
        
        public bool MoveUnit( Position from, Position to )
        {
            if (GetUnitAt(to) != null)
            {
                return false;
            }
            IUnit unit = GetUnitAt(from);
            _units.Remove(from);
            _units.Add(to, unit);
            return true;
        }

        public void EndOfTurn()
        {
            switch (PlayerInTurn)
            {
                case Player.RED:
                    PlayerInTurn = Player.BLUE;
                    break;
                case Player.BLUE:
                    PlayerInTurn = Player.RED;
                    Age = Age + 100;
                    break;
            }
        }
        
        public void ChangeWorkForceFocusInCityAt( Position position, String balance ) {}

        public void ChangeProductionInCityAt( Position position, String unitType ) {}

        public void PerformUnitActionAt( Position position ) {}
    }
}