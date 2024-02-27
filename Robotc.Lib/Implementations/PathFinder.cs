using System.Drawing;

namespace Robotc.Lib;

public class PathFinder : IPathFinder
{
    private class PathStep 
    {
        public PathStep(Point position) 
        {
            Position = position;
        }

        public Point Position;
        public PathStep? PreviousStep;
    }

    private readonly ITableTop _tableTop;
    private readonly Dictionary<Point, bool> _labeled = new ();
    private readonly Size[] _neighbourOffsets = new[] 
        {
            new Size(0, 1),
            new Size(1, 0),
            new Size(0, -1),
            new Size(-1, 0)
        };

    public PathFinder(ITableTop tableTop) => _tableTop = tableTop;

    private List<PathStep> GetNeighbours(PathStep step)
    {
        var result = new List<PathStep>();
        foreach (var offset in _neighbourOffsets)
        {
            var neighbour = new PathStep(step.Position + offset);
            if (_tableTop.IsValidPosition(neighbour.Position)) 
                result.Add(neighbour);
        }
        return result;
    }

    private IEnumerable<Point> UnwindPath(PathStep step)
    {
        var path = new List<Point>();
        PathStep? current = step;
        while (current != null)
        {
            path.Add(current.Position);
            current = current.PreviousStep;
        }
        path.Reverse();
        return path;
    }

    public bool Search(Point start, Point goal, out IEnumerable<Point>? path)
    {
        path = null;
        var current = new PathStep(start);
        Queue<PathStep> queue = new ();
        _labeled.Add(start, true);
        queue.Enqueue(current);
        while (queue.Count > 0)
        {
            current = queue.Dequeue();
            if (current.Position == goal)
            {
                path = UnwindPath(current);
                return true;
            }
            foreach (var neighbour in GetNeighbours(current))
            {
                if (_labeled.TryAdd(neighbour.Position, true))
                {
                    neighbour.PreviousStep = current;
                    queue.Enqueue(neighbour);
                }
            }
        }
        return false;
    }
}
