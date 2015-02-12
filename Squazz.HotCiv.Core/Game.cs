using System;
using System.Collections.Generic;
using System.Linq;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }

        private readonly ICity _redCity;
        private readonly ICity _blueCity;
        private readonly IAgeStrategy _ageStrategy;
        private readonly IWinningStrategy _winningStrategy;
        private readonly IActionStrategy _actionStrategy;
        private readonly IWorldLayoutStrategy _worldLayoutStrategy;

        private readonly Dictionary<Position, ICity> _cities;
        private readonly Dictionary<Position, IUnit> _units;
        private readonly Dictionary<Position, ITile> _tiles;
        private readonly Dictionary<Position, IUnit> _fortifiedArchers = new Dictionary<Position, IUnit>();
        
        public Game(IAgeStrategy ageStrategy, IWinningStrategy winningStrategy, IWorldLayoutStrategy worldLayoutStrategy, IActionStrategy actionStrategy = null)
        {
            _worldLayoutStrategy    = worldLayoutStrategy;
            _ageStrategy            = ageStrategy;
            _winningStrategy        = winningStrategy;
            _actionStrategy         = actionStrategy;

            _cities     = _worldLayoutStrategy.CreateCities();
            _units      = _worldLayoutStrategy.CreateUnits();
            _tiles      = _worldLayoutStrategy.CreateTiles();

            PlayerInTurn = Player.RED;
            Age = -4000;

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
            return _winningStrategy.GetWinner(Age, _cities);
        }
        
        public bool MoveUnit( Position from, Position to )
        {
            if (GetUnitAt(from) == null) return false;
            if (!Equals(GetUnitAt(from).Owner, PlayerInTurn)) return false;
            if (!ValidPlaceForUnit(to))
                return false;

            IUnit unit = GetUnitAt(from);
            if (Math.Abs(to.Column - from.Column) > unit.Moves || Math.Abs(to.Row - from.Row) > unit.Moves)
                return false;

            IUnit otherUnit = GetUnitAt(to);
            ICity targetCity = GetCityAt(to);
            if (otherUnit != null)
            {
                if (Equals(otherUnit.Owner, unit.Owner))
                {
                    // We cannot have two units at the same tile
                    return false;
                }
                // If the other unit is an enemy, attack and destroy it
                _units.Remove(to);
            }

            if (targetCity != null)
            {
                if (!Equals(targetCity.Owner, unit.Owner))
                {
                    // If target city is an enemt, capture it
                    _cities.Remove(to);
                    _cities.Add(to, new City(Player.RED, to));
                }
            }
            _units.Remove(from);
            _units.Add(to, unit);
            unit.Moves = 0;
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
                    // First, create units
                    CreateUnits();

                    // Then add production
                    _redCity.Vault = _redCity.Vault + 6;
                    _blueCity.Vault = _blueCity.Vault + 6;
                    
                    // Lastly advance age and change PlayerInTurn
                    Age = _ageStrategy.CalculateNewAge(Age);
                    PlayerInTurn = Player.RED;

                    // Replenish the units movements
                    foreach (var unit in _units.Where(unit => !_fortifiedArchers.ContainsKey(unit.Key))) { unit.Value.Moves = 1; }
                    break;
            }
        }
        
        public void ChangeWorkForceFocusInCityAt( Position position, String balance ) {}

        public void ChangeProductionInCityAt(Position position, String unitType)
        {
            GetCityAt(position).Production = unitType;
        }

        public void PerformUnitActionAt(Position position)
        {
            if(_actionStrategy.PerformAction(position, this))
            {
                if (GetUnitAt(position).Type == GameConstants.Archer)
                {
                    if (_fortifiedArchers.ContainsKey(position))
                        _fortifiedArchers.Remove(position);
                    else
                        _fortifiedArchers.Add(position, GetUnitAt(position));
                }
                if (GetUnitAt(position).Type == GameConstants.Settler)
                {
                    _cities.Add(position, new City(GetUnitAt(position).Owner,position));
                    _units.Remove(position);
                }
            }
        }

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