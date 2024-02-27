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

    [Fact]
    public void RotateTowardsRotatesRobotAndReturnsTurnCountSouthToNorth()
    {
        var position = new Point(2, 2);
        var neighbour = new Point(2, 3);
        IRobot sut = new Robot(position, Direction.South);
    
        Assert.True(sut.RotateTowards(neighbour, out var turn, out var count));
        Assert.Equal(Turn.Right, turn);
        Assert.Equal(2, count);


        Assert.Equal(Direction.North, sut.Heading);

    }

    [Fact]
    public void RotateTowardsRotatesRobotAndReturnsTurnCountEastToNorth()
    {
        var position = new Point(2, 2);
        var neighbour = new Point(2, 3);
        IRobot sut = new Robot(position, Direction.East);
    
        Assert.True(sut.RotateTowards(neighbour, out var turn, out var count));
        Assert.Equal(Turn.Left, turn);
        Assert.Equal(1, count);

        Assert.Equal(Direction.North, sut.Heading);

    }

    [Fact]
    public void RotateTowardsDoesntRotateRobotWhenAlreadyFacingTheCorrectDirection()
    {
        var position = new Point(2, 2);
        var neighbour = new Point(2, 3);
        IRobot sut = new Robot(position, Direction.North);
    
        Assert.True(sut.RotateTowards(neighbour, out var turn, out var count));
        Assert.Equal(Turn.Right, turn);
        Assert.Equal(0, count);

        Assert.Equal(Direction.North, sut.Heading);

    }
}