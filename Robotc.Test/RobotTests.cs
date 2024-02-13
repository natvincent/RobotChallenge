using System.Drawing;

namespace Robotc.Test;

public class RobotTests 
{
    [Fact]
    public void HasAPositionAndHeading()
    {
        IRobot sut = new Robot(new Point(1, 1), Direction.North);

        Assert.Equal(new Point(1, 1), sut.Position);
        Assert.Equal(Direction.North, sut.Heading);
    }

    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void CanTurnLeft(Direction startHeading, Direction expected)
    {
        IRobot sut = new Robot(new Point(1, 1), startHeading);

        sut.TurnLeft();

        Assert.Equal(expected, sut.Heading); 

    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    public void CanTurnRight(Direction startHeading, Direction expected)
    {
        IRobot sut = new Robot(new Point(1, 1), startHeading);

        sut.TurnRight();

        Assert.Equal(expected, sut.Heading); 

    }

    [Fact]
    public void CanCalcMoveBasedOnPositionAndHeading()
    {
        IRobot sut = new Robot(new Point(1, 1), Direction.North);

        var newPosition = sut.CalcMove();

        Assert.Equal(new Point(1, 2), newPosition);
    }
}