using System.Drawing;

namespace Robotc.Lib;

public class TableTop : ITableTop
{
    private readonly IRobotFactory _factory;
    private IRobot _robot;

    public bool HasRobot { get; private set; }

    public TableTop(IRobotFactory factory)
    { 
        _factory = factory;
        _robot = _factory.CreateNullRobot();
    }

    public Rectangle Bounds { get; set; } = new Rectangle(
        new Point(0, 0), 
        new Size(3, 3)
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

    public bool IsValidPosition(Point position) 
    {
        return Bounds.Contains(position);
    }

}