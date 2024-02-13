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

    public void Rotate(Turn turn)
    {
        Heading = Heading.Rotate(turn);
    }

    public Point CalcMove(int distance = 1)
    {
        return Heading switch //switch statement compiles to jump table, faster than array lookup
        {
            Direction.West => Position + new Size(-distance, 0),
            Direction.North => Position + new Size(0, distance),
            Direction.East => Position + new Size(distance, 0),
            Direction.South => Position + new Size(0, -distance),
            _ => Position
        };
    }
}