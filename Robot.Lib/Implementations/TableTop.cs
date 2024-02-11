using System.Drawing;

namespace Robot.Lib;

public class TableTop : ITableTop
{
    public Rectangle Bounds { get; set; } = new Rectangle(new Point(0, 0), new Size(3, 3));

    public Size Size 
    {
        get => Bounds.Size; 
        set => Bounds = new Rectangle(new Point(0, 0), value);
    }

    public bool IsValidPosition(Point position) 
    {
        return Bounds.Contains(position);
    }

}