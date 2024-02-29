using System.Drawing;
using Microsoft.VisualBasic;

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

    [Fact]
    public void CloneReturnsRobotWithSameProperties()
    {
        var position = new Point(2, 2);
        IRobot sut = new Robot(position, Direction.South);
    
        var newRobot = sut.Clone();

        Assert.Equal(position, newRobot.Position);
        Assert.Equal(Direction.South, newRobot.Heading);
    }

    [Theory]
    [InlineData(Direction.South, Direction.North, Turn.Left, 2)]
    [InlineData(Direction.South, Direction.West, Turn.Right, 1)]
    [InlineData(Direction.South, Direction.East, Turn.Left, 1)]
    [InlineData(Direction.South, Direction.South, Turn.Right, 0)]
    [InlineData(Direction.East, Direction.North, Turn.Left, 1)]
    [InlineData(Direction.East, Direction.West, Turn.Left, 2)]
    [InlineData(Direction.East, Direction.South, Turn.Right, 1)]
    [InlineData(Direction.East, Direction.East, Turn.Right, 0)]
    [InlineData(Direction.West, Direction.East, Turn.Right, 2)]
    [InlineData(Direction.West, Direction.North, Turn.Right, 1)]
    [InlineData(Direction.West, Direction.South, Turn.Left, 1)]
    [InlineData(Direction.West, Direction.West, Turn.Right, 0)]
    [InlineData(Direction.North, Direction.South, Turn.Right, 2)]
    [InlineData(Direction.North, Direction.West, Turn.Left, 1)]
    [InlineData(Direction.North, Direction.East, Turn.Right, 1)]
    [InlineData(Direction.North, Direction.North, Turn.Right, 0)]
    public void RotateTowardsRotatesRobotAndReturnsTurnCount(Direction startHeading, Direction endHeading, Turn expectedTurn, int expectedCount)
    {
        var position = new Point(2, 2);
        IRobot sut = new Robot(position, endHeading);
    
        var neighbour = sut.CalcMove();
        sut.Heading = startHeading;

        Assert.True(sut.RotateTowards(neighbour, out var turn, out var count));
        Assert.Equal(expectedTurn, turn);
        Assert.Equal(expectedCount, count);

        Assert.Equal(endHeading, sut.Heading);

    }
}