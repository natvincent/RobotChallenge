using System.Numerics;
using System.Reflection.Metadata;

namespace Robotc.Lib;

public enum Direction { West, North, East, South };

public static class DirectionHelper 
{
    private static readonly Direction s_minDirection = Enum.GetValues<Direction>().Cast<Direction>().Min(); 
    private static readonly Direction s_maxDirection = Enum.GetValues<Direction>().Cast<Direction>().Max(); 

    private static Direction NormaliseDirection(Direction direction)
    {
        if (direction < s_minDirection)
            return s_minDirection;
        else if (direction > s_maxDirection)
            return s_maxDirection;
        else
            return direction;
    }
    public static Direction ToLeft(this Direction direction)
    {
        direction = NormaliseDirection(direction);
        if (direction == s_minDirection)
            return s_maxDirection;
        return --direction;
    }

    public static Direction ToRight(this Direction direction)
    {
        direction = NormaliseDirection(direction);
        if (direction == s_maxDirection)
            return s_minDirection;
        return ++direction;
    }

}