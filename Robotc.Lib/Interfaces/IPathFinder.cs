using System.Drawing;

namespace Robotc.Lib;

public interface IPathFinder
{
    public bool Search(Point start, Point goal, out IEnumerable<Point>? path);
}