namespace Squazz.HotCiv.Strategies
{
    public interface IWinningStrategy
    {
        Player? GetWinner(int age);
    }
}