namespace Robotc.Test;

public class DirectionTests
{
    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void ToLeftWrapsCorrectly(Direction start, Direction expected)
    {
        var actual = start.ToLeft();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData((Direction) (-1), Direction.South)]
    [InlineData((Direction) 4, Direction.East)]
    public void ToLeftHandlesOutOfBoundsValues(Direction start, Direction expected)
    {
        var actual = start.ToLeft();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    [InlineData((Direction) 4, Direction.West)]
    [InlineData((Direction) (-1), Direction.North)]
    public void ToRightWrapsCorrectly(Direction start, Direction expected)
    {
        var actual = start.ToRight();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData((Direction) 4, Direction.West)]
    [InlineData((Direction) (-1), Direction.North)]
    public void ToRightHandlesOutOfBoundsValues(Direction start, Direction expected)
    {
        var actual = start.ToRight();
        Assert.Equal(expected, actual);
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
    public void RotateExtensionMethod(Direction start, Turn turn, Direction expected)
    {
        var actual = start.Rotate(turn);
        Assert.Equal(expected, actual);
    }
}