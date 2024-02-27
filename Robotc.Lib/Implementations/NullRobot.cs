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

    public void Rotate(Turn turn)
    {
    }

     public bool RotateTowards(Point neighbour, out Turn turn, out int count)
    {
        count = 0;
        turn = Turn.Right;
        return false;
    }

   public IRobot Clone()
    {
        return new NullRobot();
    }

   public Point CalcMove(int distance = 1)
    {
        return Point.Empty;
    }

}