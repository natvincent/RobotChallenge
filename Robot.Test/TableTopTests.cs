using System.Drawing;

namespace Robot.Test;

public class TableTopTests
{
    [Fact]
    public void HasDefaultSize()
    {
        ITableTop sut = new TableTop();

        Assert.Equal(new Size(3, 3), sut.Size);
        Assert.Equal(new Rectangle(0, 0, 3, 3), sut.Bounds);

    }

    [Theory]
    [InlineData(0, 0, true)] 
    [InlineData(-1, 0, false)] 
    [InlineData(0, -1, false)] 
    [InlineData(1, 1, true)] 
    [InlineData(2, 2, true)]
    [InlineData(3, 3, false)]
    [InlineData(0, 3, false)]
    [InlineData(3, 0, false)]
   public void ReturnsWhetherNewPositionIsValid(int x, int y, bool expected)
    {
        ITableTop sut = new TableTop();

        Assert.Equal(expected, sut.IsValidPosition(new Point(x, y)));
    }
}