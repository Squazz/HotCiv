using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }

        private readonly Dictionary<Position, ICity> _cities = new Dictionary<Position, ICity>();
        private readonly Dictionary<Position, IUnit> _units = new Dictionary<Position, IUnit>();
        private readonly Dictionary<Position, ITile> _tiles = new Dictionary<Position, ITile>();
        
        public Game()
        {
            PlayerInTurn = Player.RED;
            Age = -4000;

            // Add Standard Cities
            _cities.Add(new Position(1, 1), new City(Player.RED));
            _cities.Add(new Position(4, 1), new City(Player.BLUE));

            // Add standard units
            _units.Add(new Position(2, 0), new Unit(Player.RED, GameConstants.Archer));
            _units.Add(new Position(3, 2), new Unit(Player.BLUE, GameConstants.Legion));
            _units.Add(new Position(4, 3), new Unit(Player.RED, GameConstants.Settler));

            // Decorate the board with tiles
            _tiles.Add(new Position(0, 1), new Tile(GameConstants.Ocean));
            _tiles.Add(new Position(1, 0), new Tile(GameConstants.Hills));
            _tiles.Add(new Position(2, 2), new Tile(GameConstants.Mountains));
            // Decorate the rest of the board with plains tiles
            for (int i = 0; i <= 15; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    Position position = new Position(i, j);
                    if (GetTileAt(position) == null)
                    {
                        _tiles.Add(position, new Tile(GameConstants.Plains));
                    }
                }
            }
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
            if (Equals(GetUnitAt(from).Owner, PlayerInTurn))
            {
                IUnit otherUnit = GetUnitAt(to);
                if (otherUnit != null)
                {
                    if (Equals(otherUnit.Owner, GetUnitAt(from).Owner))
                    {
                        // We cannot have two units at the same tile
                        return false;
                    }
                    else
                    {
                        // If the other unit is an enemy, attack and destroy it
                        _units.Remove(to);
                    }
                }
                IUnit unit = GetUnitAt(from);
                _units.Remove(from);
                _units.Add(to, unit);
                return true;
            }
            else
            {
                return false;
            }
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