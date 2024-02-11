using System.Drawing;

namespace Robot.Lib;

public enum Direction { Up, Left, Down, Right };

public interface IRobot 
{
    Point Position { get; set; }
    Direction Heading { get; set; }
}