using System.Drawing;

namespace Robotc.Lib;

public interface IRobot 
{
    Point Position { get; set; }
    Direction Heading { get; set; }

    void Rotate(Turn turn);

    Point CalcMove(int distance = 1);
}