using System.Drawing;

namespace Robotc.Lib;

public class NullRobot : IRobot
{
    public Point Position { get => Point.Empty; set { } }
    public Direction Heading { get => Direction.North; set { } }

    public void TurnLeft() 
    {
    }

    public void TurnRight() 
    {
    }

    public Point CalcMove(int distance = 1)
    {
        return Point.Empty;
    }

}