using System.Drawing;

namespace Robotc.Test;

public class NullRobotTests
{
    [Fact]
    public void SettingPositionDoesNothing()
    {
        IRobot sut = new NullRobot();

        Assert.Equal(Point.Empty, sut.Position);

        sut.Position = new Point(1, 1);

        Assert.Equal(Point.Empty, sut.Position);
    }

    [Fact]
    public void SettingHeadingDoesntChangeFromNorth()
    {
        IRobot sut = new NullRobot();

        Assert.Equal(Direction.North, sut.Heading);

        sut.Heading = Direction.East;

        Assert.Equal(Direction.North, sut.Heading);

        sut.Heading = Direction.West;

        Assert.Equal(Direction.North, sut.Heading);

        sut.Heading = Direction.South;

        Assert.Equal(Direction.North, sut.Heading);
    }

    [Fact]
    public void CalcMoveAlwaysReturnsEmptyPosition()
    {
        IRobot sut = new NullRobot();

        Assert.Equal(Point.Empty, sut.Position);

        sut.Position = new Point(1, 1);

        Assert.Equal(Point.Empty, sut.Position);
    }
}