﻿using System;
using System.Collections.Generic;

namespace Squazz.HotCiv
{
    public class GameImpl : IGame {

        private Player _player   = Player.RED;
        private int _age = 4000;

        public ITile GetTileAt(Position p)
        {
            Position ocean = new Position(1,0);
            Position hill = new Position(0,1);
            Position mountain = new Position(2,2);
            if (p.equals(ocean))
            {
                return new TileImpl(p, "ocean");
            }
            else if (p.equals(hill))
            {
                return new TileImpl(p, "hill");
            }
            else if (p.equals(mountain))
            {
                return new TileImpl(p, "mountain");
            }
            else
            {
                return new TileImpl(p, "plain");
            }
        }

        public IUnit GetUnitAt(Position p)
        {
            if (p.Equals(new Position(0, 2)))
            {
                return new UnitImpl(Player.RED, "archer");
            }
            else
            {
                return null;
            }
        }

        public ICity GetCityAt(Position p) { return new CityImpl(_player); }

        public Player GetPlayerInTurn() { return _player; }

        public Player GetWinner() { return _player; }

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
                    _age = _age - 100;
                    break;
            }
        }

        public void EndRounds(int rounds=1)
        {
            for (int i = 1; i <= rounds; i++)
            {
                EndOfTurn();
                EndOfTurn();
            }
        }

        public void ChangeWorkForceFocusInCityAt( Position p, String balance ) {}

        public void ChangeProductionInCityAt( Position p, String unitType ) {}

        public void PerformUnitActionAt( Position p ) {}

    }
}