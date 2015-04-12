using System;
using System.Collections.Generic;
using System.Linq;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }

        private readonly IAgeStrategy _ageStrategy;
        private readonly IWinningStrategy _winningStrategy;
        private readonly IActionStrategy _actionStrategy;
        private readonly IAttackStrategy _attackStrategy;

        private readonly Dictionary<Position, ICity> _cities;
        private readonly Dictionary<Position, IUnit> _units;
        private readonly Dictionary<Position, ITile> _tiles;
        private readonly Dictionary<Position, IUnit> _fortifiedArchers = new Dictionary<Position, IUnit>();
        
        public Game(IAgeStrategy ageStrategy, IWinningStrategy winningStrategy, IWorldLayoutStrategy worldLayoutStrategy, IAttackStrategy attackStrategy, IActionStrategy actionStrategy = null)
        {
            var layoutStrategy = worldLayoutStrategy;
            _attackStrategy         = attackStrategy;
            _ageStrategy            = ageStrategy;
            _winningStrategy        = winningStrategy;
            _actionStrategy         = actionStrategy;

            _cities     = layoutStrategy.CreateCities();
            _units      = layoutStrategy.CreateUnits();
            _tiles      = layoutStrategy.CreateTiles();

            // RED starts the game
            PlayerInTurn = Player.RED;
            Age = -4000;
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

            var unit = GetUnitAt(from);
            if (Math.Abs(to.Column - from.Column) > unit.Moves || Math.Abs(to.Row - from.Row) > unit.Moves)
                return false;

            var otherUnit = GetUnitAt(to);
            var targetCity = GetCityAt(to);
            if (otherUnit != null)
            {
                if (Equals(otherUnit.Owner, unit.Owner))
                {
                    // We cannot have two units at the same tile
                    return false;
                }
                // If the other unit is an enemy, attack it
                var winner = _attackStrategy.Attack(from, to, _cities, _units);
                if (winner.Owner == unit.Owner)
                {
                    _units.Remove(from);
                    _units.Add(to, unit);
                    unit.Moves = 0;
                }
                else
                {
                    _units.Remove(from);
                }
                return true;
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
                    foreach (var city in _cities) { city.Value.Vault += 6; }
                    
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
            if (!_actionStrategy.PerformAction(position, this)) return;
            switch (GetUnitAt(position).Type)
            {
                case GameConstants.Archer:
                    if (_fortifiedArchers.ContainsKey(position))
                        _fortifiedArchers.Remove(position);
                    else
                        _fortifiedArchers.Add(position, GetUnitAt(position));
                    break;
                case GameConstants.Settler:
                    _cities.Add(position, new City(GetUnitAt(position).Owner,position));
                    _units.Remove(position);
                    break;
            }
        }

        public int GetActualUnitAttack(Position unitPosition)
        {
            var unit = GetUnitAt(unitPosition);
            var attack = unit.Attack + GetAmountOfNearbyUnits(unitPosition);

            if (GetCityAt(unitPosition) != null)
                attack *= 3; // Multiply by 3 if the unit is in a city

            if (GetTileAt(unitPosition).Type == GameConstants.Hills 
                || GetTileAt(unitPosition).Type == GameConstants.Forest)
                attack *= 2; // If the unit is at a hill or in a forrest, double the attack
            
            return attack;
        }
        public int GetActualUnitDefence(Position unitPosition)
        {
            var unit = GetUnitAt(unitPosition);
            var defence = unit.Defense + GetAmountOfNearbyUnits(unitPosition);

            if (GetCityAt(unitPosition) != null)
                defence *= 3; // Multiply by 3 if the unit is in a city

            if (GetTileAt(unitPosition).Type == GameConstants.Hills 
                || GetTileAt(unitPosition).Type == GameConstants.Forest)
                defence *= 2; // If the unit is at a hill or in a forrest, double the defence

            return defence;
        }

        private void CreateUnits()
        {
            foreach (var city in _cities)
            {
                if (!WeCanProduce(city.Value)) continue;
                var position = EnsureProperUnitPlacement(city.Value.Position);

                _units.Add(position, new Unit(city.Value.Owner, city.Value.Production));
                city.Value.Vault -= 10;
                city.Value.Production = null;
            }
        }

        private static bool WeCanProduce(ICity city)
        {
            var wealth = city.Vault;
            var production = city.Production;

            if (production == null) return false;

            var unitPrice = 10; // Assume we are producing an archer
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

        private int GetAmountOfNearbyUnits(Position unitPosition)
        {
            var amount = 0;

            if (_units.ContainsKey(new Position(unitPosition.Row - 1, unitPosition.Column - 1)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row - 1, unitPosition.Column)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row - 1, unitPosition.Column + 1)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row, unitPosition.Column - 1)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row, unitPosition.Column + 1)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row + 1, unitPosition.Column - 1)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row + 1, unitPosition.Column)))
                amount++;
            if (_units.ContainsKey(new Position(unitPosition.Row + 1, unitPosition.Column + 1)))
                amount++;

            return amount;
        }

        private bool ValidPlaceForUnit(Position position)
        {
            return 
                !Equals(GetTileAt(position).Type, GameConstants.Mountains) && 
                !Equals(GetTileAt(position).Type, GameConstants.Ocean);
        }
    }
}