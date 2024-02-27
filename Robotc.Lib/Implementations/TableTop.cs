using System.Drawing;

namespace Robotc.Lib;

public class TableTop : ITableTop
{
    private const int DefaultWidth = 5;
    private const int DefaultHeight = 5;

    private readonly IRobotFactory _factory;
    private readonly List<Point> _obstacles = new ();
    private IRobot _robot;

    private bool PointFreeOfObstacles(Point position)
    {
        foreach (var obstacle in _obstacles)
        {
            if (position == obstacle)
            {
                return false;
            }
        }
        return true;
    }

    private bool PointCanHaveObstacle(Point position)
    {
        return IsValidPosition(position)
          && _robot.Position != position;
    }

    public bool HasRobot { get; private set; }

    public TableTop(IRobotFactory factory)
    { 
        _factory = factory;
        _robot = _factory.CreateNullRobot();
    }

    public Rectangle Bounds { get; set; } = new Rectangle(
        new Point(0, 0), 
        new Size(DefaultWidth, DefaultHeight)
    );

    public Size Size 
    {
        get => Bounds.Size; 
        set => Bounds = new Rectangle(new Point(0, 0), value);
    }
    public IRobot Robot { get => _robot; }

    public bool PlaceRobot(Point position, Direction heading)
    {
        if (IsValidPosition(position))
        {
            _robot = _factory.CreateRobot(position, heading);
            HasRobot = true;
            return true;
        }  
        return false;
    }

    public bool PlaceObstacle(Point position)
    {
        if (PointCanHaveObstacle(position))
        {
            _obstacles.Add(position);    
            return true;
        }
        return false;
    }

    public bool IsValidPosition(Point position) 
    {
        return Bounds.Contains(position)
            && PointFreeOfObstacles(position);
    }

}