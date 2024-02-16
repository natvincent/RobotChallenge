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
    [InlineData(Direction.North, Turn.Left, Direction.West)]
    [InlineData(Direction.West, Turn.Left, Direction.South)]
    [InlineData(Direction.South, Turn.Left, Direction.East)]
    [InlineData(Direction.East, Turn.Left, Direction.North)]
    [InlineData(Direction.North, Turn.Right, Direction.East)]
    [InlineData(Direction.East, Turn.Right, Direction.South)]
    [InlineData(Direction.South, Turn.Right, Direction.West)]
    [InlineData(Direction.West, Turn.Right, Direction.North)]
    public void CanRotate(Direction startHeading, Turn turn,  Direction expected)
    {
        IRobot sut = new Robot(new Point(1, 1), startHeading);

        sut.Rotate(turn);

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