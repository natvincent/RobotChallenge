using System.Drawing;

namespace Robotc.Lib;

public interface ITableTop
{
    Rectangle Bounds { get; set; }
    Size Size { get; set; }

    IRobot Robot { get; }

    bool HasRobot { get; }

    bool PlaceRobot(Point position, Direction heading);

    bool PlaceObstacle(Point position);

    bool IsValidPosition(Point position);

}