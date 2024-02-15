namespace Robotc.Lib;

using System.Drawing;

public class RobotFactory : IRobotFactory
{
    public IRobot CreateNullRobot()
    {
        return new NullRobot();
    }

    public IRobot CreateRobot(Point position, Direction heading)
    {
        return new Robot(position, heading);
    }
}