using System.Drawing;

namespace Robotc.Lib;

public interface IRobot 
{
    Point Position { get; set; }
    Direction Heading { get; set; }

    void TurnLeft();
    void TurnRight();

    Point CalcMove(int distance = 1);
}