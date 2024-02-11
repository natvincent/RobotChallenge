using System.Drawing;

namespace Robot.Lib;

public class Robot : IRobot
{
    public Robot(Point position, Direction heading) 
    {
        Position = position;
        Heading = heading;
    }
    
    public Point Position { get; set; }
    public Direction Heading { get; set; }
}