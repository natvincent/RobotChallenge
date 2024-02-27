using System.Drawing;

namespace Robotc.Test;

public class PathFinderTests : BaseCommandTests
{

    private void SetupTableTop(Point[] points)
    {
        _tableTop.Setup(mock => mock.IsValidPosition(It.IsIn(points)))
            .Returns(true);
        _tableTop.Setup(mock => mock.IsValidPosition(It.IsNotIn(points)))
            .Returns(false);
    }

    private void SearchAndAssert(Point fromPosition, Point toPosition, Point[] expectedResult)
    {

        IPathFinder sut = new PathFinder(_tableTop.Object);

        IEnumerable<Point>? path;
        Assert.True(sut.Search(fromPosition,  toPosition, out path));
        Assert.NotNull(path);
        Assert.Equal(
            path,
            expectedResult
        );
        _tableTop.Verify(mock => mock.IsValidPosition(It.IsAny<Point>()), Times.AtLeastOnce);
    }

    [Fact]
    public void CanFindPathToNeighbouringPosition()
    {
        var fromPosition = new Point(0, 0);
        var toPosition = new Point(0, 1);

        var validPoints = new Point[] 
        {
            new Point(0, 1),
            new Point(1, 0)
        };

        SetupTableTop(validPoints);

        SearchAndAssert(fromPosition, toPosition, [new (0, 0), new (0, 1)]);

    }

    [Fact]
    public void CanFindPathToOtherSizeOfTableTop()
    {
        var fromPosition = new Point(0, 0);
        var toPosition = new Point(4, 4);

        var validPoints = new Point[] 
        {
            new (0, 4), new (1, 4), new (2, 4), new (3, 4), new (4, 4),
            new (0, 3), new (1, 3), new (2, 3), new (3, 3), new (4, 3),
            new (0, 2), new (1, 2), new (2, 2), new (3, 2), new (4, 2),
            new (0, 1), new (1, 1), new (2, 1), new (3, 1), new (4, 1),
            new (0, 0), new (1, 0), new (2, 0), new (3, 0), new (4, 0)
        };
        SetupTableTop(validPoints);

        SearchAndAssert(
            fromPosition, 
            toPosition, 
            [
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),
                new Point(0,3),
                new Point(0,4),
                new Point(1,4),
                new Point(2,4),
                new Point(3,4),
                new Point(4,4)
            ]);        
        
    }

    [Fact]
    public void CanFindPathToOtherSizeOfTableTopWhenThereAreObstacles()
    {

        var fromPosition = new Point(0, 0);
        var toPosition = new Point(4, 4);

        var validPoints = new Point[] 
        {
            new (0, 4), new (1, 4), new (2, 4), new (3, 4), new (4, 4),
                                    new (2, 3), new (3, 3), new (4, 3),
            new (0, 2), new (1, 2), new (2, 2), new (3, 2), new (4, 2),
            new (0, 1), new (1, 1),             new (3, 1), new (4, 1),
            new (0, 0), new (1, 0),             new (3, 0), new (4, 0)
        };

        SetupTableTop(validPoints);
        
        SearchAndAssert(
            fromPosition, 
            toPosition, 
            [
                new Point(0,0),
                new Point(0,1),
                new Point(0,2),
                new Point(1,2),
                new Point(2,2),
                new Point(2,3),
                new Point(2,4),
                new Point(3,4),
                new Point(4,4)
            ]);

    }

    [Fact]
    public void CanFindPathToLowerRightCornerWhenThereAreObstacles()
    {

        var fromPosition = new Point(0, 0);
        var toPosition = new Point(4, 0);

        var validPoints = new Point[] 
        {
            new (0, 4), new (1, 4), new (2, 4), new (3, 4), 
                        new (1, 3),             new (3, 3), 
                        new (1, 2),             new (3, 2), 
                        new (1, 1),             new (3, 1), 
            new (0, 0), new (1, 0),             new (3, 0), new (4, 0)
        };

        SetupTableTop(validPoints);
        
        SearchAndAssert(
            fromPosition, 
            toPosition, 
            [
                new Point(0,0),
                new Point(1,0),
                new Point(1,1),
                new Point(1,2),
                new Point(1,3),
                new Point(1,4),
                new Point(2,4),
                new Point(3,4),
                new Point(3,3),
                new Point(3,2),
                new Point(3,1),
                new Point(3,0),
                new Point(4,0)
            ]);

    }    
}