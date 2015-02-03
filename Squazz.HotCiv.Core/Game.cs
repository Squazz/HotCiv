using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class GameImpl : IGame {

        private Player _player   = Player.RED;
        private int _age = -4000;

        public ITile GetTileAt(Position p)
        {
            Position ocean = new Position(1,0);
            Position hill = new Position(0,1);
            Position mountain = new Position(2,2);
            if (p.equals(ocean))
            {
                return new Tile(p, "ocean");
            }
            else if (p.equals(hill))
            {
                return new Tile(p, "hill");
            }
            else if (p.equals(mountain))
            {
                return new Tile(p, "mountain");
            }
            else
            {
                return new Tile(p, "plain");
            }
        }

        public IUnit GetUnitAt(Position p)
        {
            if (p.Equals(new Position(0, 2)))
            {
                return new Unit(Player.RED, "archer");
            }
            else if (p.Equals(new Position(3, 2)))
            {
                return new Unit(Player.BLUE, "legion");
            }
            else
            {
                return null;
            }
        }

        public ICity GetCityAt(Position p) { return new CityImpl(_player); }

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
        
        public void ChangeWorkForceFocusInCityAt( Position p, String balance ) {}

        public void ChangeProductionInCityAt( Position p, String unitType ) {}

        public void PerformUnitActionAt( Position p ) {}

    }
}