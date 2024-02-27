using System.Drawing;

namespace Robotc.Lib;

public interface IRobotFactory
{
    IRobot CreateRobot(Point position, Direction heading);
    IRobot CreateNullRobot();
    IPathFinder CreatePathFinder(ITableTop tableTop);
}