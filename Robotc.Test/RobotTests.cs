using System.Drawing;

namespace Robotc.Test;

public class RobotTests 
{
    [Fact]
    public void HasAPositionAndHeading()
    {
        IRobot sut = new Robot(new Point(1, 1), Direction.Up);

        Assert.Equal(new Point(1, 1), sut.Position);
        Assert.Equal(Direction.Up, sut.Heading);
    }

    [Theory]
    [InlineData(Direction.Up, Direction.Left)]
    [InlineData(Direction.Left, Direction.Down)]
    [InlineData(Direction.Down, Direction.Right)]
    [InlineData(Direction.Right, Direction.Up)]
    public void CanTurnLeft(Direction startHeading, Direction expected)
    {
        IRobot sut = new Robot(new Point(1, 1), startHeading);

        sut.TurnLeft();

        Assert.Equal(expected, sut.Heading); 

    }

    [Theory]
    [InlineData(Direction.Up, Direction.Right)]
    [InlineData(Direction.Right, Direction.Down)]
    [InlineData(Direction.Down, Direction.Left)]
    [InlineData(Direction.Left, Direction.Up)]
    public void CanTurnRight(Direction startHeading, Direction expected)
    {
        IRobot sut = new Robot(new Point(1, 1), startHeading);

        sut.TurnRight();

        Assert.Equal(expected, sut.Heading); 

    }

    [Fact]
    public void CanCalcMoveBasedOnPositionAndHeading()
    {
        IRobot sut = new Robot(new Point(1, 1), Direction.Up);

        var newPosition = sut.CalcMove();

        Assert.Equal(new Point(1, 2), newPosition);
    }
}