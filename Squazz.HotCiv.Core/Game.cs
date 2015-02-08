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
            _cities.Add(new Position(1, 1), new City(Player.RED, new Position(1, 1)));
            _cities.Add(new Position(4, 1), new City(Player.BLUE, new Position(4, 1)));

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
                    if (!ValidPlaceForUnit(to))
                        return false;
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

        private void CreateUnits()
        {
            if (WeCanProduce(_redCity))
            {
                Position position = EnsureProperUnitPlacement(_redCity.Position);

                _units.Add(position, new Unit(_redCity.Owner, _redCity.Production));
                _redCity.Vault = _redCity.Vault - 10;
                _redCity.Production = null;
            }

            if (WeCanProduce(_blueCity))
            {
                Position position = EnsureProperUnitPlacement(_blueCity.Position);

                _units.Add(position, new Unit(_blueCity.Owner, _blueCity.Production));
                _blueCity.Vault = _blueCity.Vault - 10;
                _blueCity.Production = null;
            }
        }

        private bool WeCanProduce(ICity city)
        {
            int wealth = city.Vault;
            String production = city.Production;

            if (production == null) return false;

            int unitPrice = 10; // Assume we are producing an archer
            if (production == GameConstants.Legion) unitPrice = 15;
            if (production == GameConstants.Settler) unitPrice = 30;

            return wealth >= unitPrice;
        }

        private Position EnsureProperUnitPlacement(Position position)
        {
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row - 1, position.Column);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row, position.Column + 1);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row + 1, position.Column);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row + 1, position.Column);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row, position.Column - 1);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row, position.Column - 1);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row - 1, position.Column);
            if (_units.ContainsKey(position) || !ValidPlaceForUnit(position))
                position = new Position(position.Row - 1, position.Column);

            return position;
        }

        private bool ValidPlaceForUnit(Position position)
        {
            return 
                !Equals(GetTileAt(position).Type, GameConstants.Mountains) && 
                !Equals(GetTileAt(position).Type, GameConstants.Ocean);
        }
    }
}