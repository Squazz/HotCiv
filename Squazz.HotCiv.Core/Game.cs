using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }

        private readonly ICity _redCity;
        private readonly ICity _blueCity;

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
            _tiles.Add(new Position(1, 0), new Tile(GameConstants.Ocean));
            _tiles.Add(new Position(0, 1), new Tile(GameConstants.Hills));
            _tiles.Add(new Position(2, 2), new Tile(GameConstants.Mountains));

            _redCity = GetCityAt(new Position(1, 1));
            _blueCity = GetCityAt(new Position(4, 1));
        }

        public ITile GetTileAt(Position position)
        {
            ITile tile;
            return _tiles.TryGetValue(position, out tile) ? tile : new Tile(GameConstants.Plains);
        }

        public IUnit GetUnitAt(Position position)
        {
            IUnit unit;
            return _units.TryGetValue(position, out unit) ? unit : null;
        }

        public ICity GetCityAt(Position position)
        {
            ICity city;
            return _cities.TryGetValue(position, out city) ? city : null;
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
            if (GetUnitAt(from) != null)
            {
                if (Equals(GetUnitAt(from).Owner, PlayerInTurn))
                {
                    if (Equals(GetTileAt(to).Type, GameConstants.Mountains))
                    {
                        return false;
                    }
                    if (Equals(GetTileAt(to).Type, GameConstants.Ocean))
                    {
                        return false;
                    }
                    IUnit otherUnit = GetUnitAt(to);
                    if (otherUnit != null)
                    {
                        if (Equals(otherUnit.Owner, GetUnitAt(from).Owner))
                        {
                            // We cannot have two units at the same tile
                            return false;
                        }
                        // If the other unit is an enemy, attack and destroy it
                        _units.Remove(to);
                    }
                    IUnit unit = GetUnitAt(from);
                    _units.Remove(from);
                    _units.Add(to, unit);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void EndOfTurn()
        {
            switch (PlayerInTurn)
            {
                case Player.RED:
                    PlayerInTurn = Player.BLUE;
                    break;
                case Player.BLUE:
                    // First, create units
                    CreateUnits();

                    // Then add production
                    _redCity.Vault = _redCity.Vault + 6;
                    _blueCity.Vault = _blueCity.Vault + 6;
                    
                    // Lastly advance age and change PlayerInTurn
                    Age = Age + 100;
                    PlayerInTurn = Player.RED;
                    break;
            }
        }
        
        public void ChangeWorkForceFocusInCityAt( Position position, String balance ) {}

        public void ChangeProductionInCityAt(Position position, String unitType)
        {
            GetCityAt(position).Production = unitType;
        }

        public void PerformUnitActionAt( Position position ) {}

        public void CreateUnits()
        {
            if (_redCity.Production != null && _redCity.Vault >= 10)
            {
                Position redPosition = new Position(1, 1);
                if (_units.ContainsKey(redPosition)) redPosition = new Position(0, 1);

                if (_redCity.Production == GameConstants.Archer)
                {
                    _units.Add(redPosition, new Unit(Player.RED, GameConstants.Archer));
                    _redCity.Vault = _redCity.Vault - 10;
                    _redCity.Production = null;
                }
                if (_redCity.Production == GameConstants.Legion && _redCity.Vault >= 15)
                {
                    _units.Add(redPosition, new Unit(Player.RED, GameConstants.Legion));
                    _redCity.Vault = _redCity.Vault - 15;
                    _redCity.Production = null;
                }
                if (_redCity.Production == GameConstants.Settler && _redCity.Vault >= 30)
                {
                    _units.Add(redPosition, new Unit(Player.RED, GameConstants.Settler));
                    _redCity.Vault = _redCity.Vault - 30;
                    _redCity.Production = null;
                }
            }

            Position bluePosition = new Position(4, 1);
            if (_units.ContainsKey(bluePosition)) bluePosition = new Position(3, 1);

            if (_blueCity.Production != null && _blueCity.Vault >= 10)
            {
                if (_blueCity.Production == GameConstants.Archer)
                {
                    _units.Add(bluePosition, new Unit(Player.BLUE, GameConstants.Archer));
                    _blueCity.Vault = _blueCity.Vault - 10;
                    _blueCity.Production = null;
                }
                if (_blueCity.Production == GameConstants.Legion && _redCity.Vault >= 15)
                {
                    _units.Add(bluePosition, new Unit(Player.BLUE, GameConstants.Legion));
                    _blueCity.Vault = _blueCity.Vault - 15;
                    _blueCity.Production = null;
                }
                if (_blueCity.Production == GameConstants.Settler && _redCity.Vault >= 30)
                {
                    _units.Add(bluePosition, new Unit(Player.BLUE, GameConstants.Settler));
                    _blueCity.Vault = _blueCity.Vault - 30;
                    _blueCity.Production = null;
                }
            }
        }
    }
}