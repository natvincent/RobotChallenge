namespace Robotc.Lib;

public enum Turn { Left = -1, Right = 1}

public static class TurnExtensions
{
    public static Turn Opposite(this Turn turn)
    {
        return turn == Turn.Right ? Turn.Left : Turn.Right;
    }
}