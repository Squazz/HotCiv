namespace Squazz.HotCiv.Strategies
{
    public interface IActionStrategy
    {
        bool PerformAction(Position position, IGame game);
    }
}