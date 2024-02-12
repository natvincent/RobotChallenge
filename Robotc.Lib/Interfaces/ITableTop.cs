using System.Drawing;

namespace Robotc.Lib;

public interface ITableTop
{
    Rectangle Bounds { get; set; }
    Size Size { get; set; }
    bool IsValidPosition(Point position);
}