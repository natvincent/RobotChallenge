namespace Robotc.Test;

public class DirectionTests
{
    [Theory]
    [InlineData(Direction.Up, Direction.Left)]
    [InlineData(Direction.Left, Direction.Down)]
    [InlineData(Direction.Down, Direction.Right)]
    [InlineData(Direction.Right, Direction.Up)]
    public void ToLeftWrapsCorrectly(Direction start, Direction expected)
    {
        var sut = start.ToLeft();
        Assert.Equal(expected, sut);
    }

    [Theory]
    [InlineData((Direction) (-1), Direction.Down)]
    [InlineData((Direction) 4, Direction.Right)]
    public void ToLeftHandlesOutOfBoundsValues(Direction start, Direction expected)
    {
        var sut = start.ToLeft();
        Assert.Equal(expected, sut);
    }

    [Theory]
    [InlineData(Direction.Up, Direction.Right)]
    [InlineData(Direction.Right, Direction.Down)]
    [InlineData(Direction.Down, Direction.Left)]
    [InlineData(Direction.Left, Direction.Up)]
    [InlineData((Direction) 4, Direction.Left)]
    [InlineData((Direction) (-1), Direction.Up)]
    public void ToRightWrapsCorrectly(Direction start, Direction expected)
    {
        var sut = start.ToRight();
        Assert.Equal(expected, sut);
    }

    [Theory]
    [InlineData((Direction) 4, Direction.Left)]
    [InlineData((Direction) (-1), Direction.Up)]
    public void ToRightHandlesOutOfBoundsValues(Direction start, Direction expected)
    {
        var sut = start.ToRight();
        Assert.Equal(expected, sut);
    }
}