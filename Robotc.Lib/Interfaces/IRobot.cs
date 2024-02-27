using System.Drawing;

namespace Robotc.Lib;

public interface IRobot 
{
    Point Position { get; set; }
    Direction Heading { get; set; }

    void Rotate(Turn turn);

    bool RotateTowards(Point neighbour, out Turn turn, out int count);

    IRobot Clone();

    Point CalcMove(int distance = 1);
}