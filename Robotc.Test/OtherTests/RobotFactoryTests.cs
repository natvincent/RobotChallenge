namespace Robotc.Test;

using System.Drawing;

public class RobotFactoryTests
{
    [Fact]
    public void FactoryReturnsProperlyInitialisedRobot()
    {
        IRobotFactory sut = new RobotFactory();
        IRobot robot = sut.CreateRobot(new Point(1, 1), Direction.North);

        Assert.NotNull(robot);
        Assert.Equal(new Point(1, 1), robot.Position);
        Assert.Equal(Direction.North, robot.Heading);
    }

    [Fact]
    public void FactoryReturnsNullRobot()
    {
        IRobotFactory sut = new RobotFactory();
        IRobot robot = sut.CreateNullRobot();

        Assert.NotNull(robot);
        Assert.Equal(Point.Empty, robot.Position);
        Assert.Equal(Direction.North, robot.Heading);

        robot.Position = new Point(1, 1);
        Assert.Equal(Point.Empty, robot.Position);

    }
}