using System;

namespace Squazz.HotCiv
{
    public class Game : IGame {
        public Player PlayerInTurn { get; private set; }
        public int Age { get; private set; }
        

        public ITile GetTileAt(Position position) { return null; }

        public IUnit GetUnitAt(Position position) { return null; }

        public ICity GetCityAt(Position position) { return null; }

        public Player? GetWinner() { return null; }

        public bool MoveUnit(Position from, Position to) { return false; }

        public void EndOfTurn() {}
        
        public void ChangeWorkForceFocusInCityAt( Position position, String balance ) {}

        public void ChangeProductionInCityAt(Position position, String unitType) {}

        public void PerformUnitActionAt( Position position ) {}
    }
}