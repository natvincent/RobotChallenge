using System.Drawing;

namespace Robotc.Lib;

public class Robot : IRobot
{
    public Robot(Point position, Direction heading) 
    {
        Position = position;
        Heading = heading;
    }
    
    public Point Position { get; set; }
    public Direction Heading { get; set; }

    public void TurnLeft() 
    {
        Heading = Heading.ToLeft();
    }

    public void TurnRight() 
    {
        Heading = Heading.ToRight();
    }

    public Point CalcMove(int distance = 1)
    {
        return Heading switch //switch statement compiles to jump table, faster than array lookup
        {
            Direction.Left => Position + new Size(-distance, 0),
            Direction.Up => Position + new Size(0, distance),
            Direction.Right => Position + new Size(distance, 0),
            Direction.Down => Position + new Size(0, -distance),
            _ => Position
        };
    }
}