namespace Squazz.HotCiv.Strategies
{
    public class ActionStrategyDoAction : IActionStrategy
    {
        public bool PerformAction(Position position, IGame game)
        {
            IUnit unit = game.GetUnitAt(position);
            switch (unit.Type)
            {
                case GameConstants.Archer:
                    if (unit.Defense == 6)
                        unit.Defense = 3;
                    else
                        unit.Defense = unit.Defense * 2;
                        unit.Moves = 0;
                    return true;
                case GameConstants.Settler:
                    return true;
            }
            return false;
        }
    }
}