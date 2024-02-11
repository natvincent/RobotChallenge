using System.Drawing;

namespace Robot.Test;

public class RobotTests 
{
    [Fact]
    public void HasAPositionAndHeading()
    {
        IRobot sut = new Robot.Lib.Robot(new Point(1, 1), Direction.Up);

        Assert.Equal(new Point(1, 1), sut.Position);
        Assert.Equal(Direction.Up, sut.Heading);
    }
}