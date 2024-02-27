using System.Drawing;

namespace Robotc.Test;

public class BlockCommandTests : BaseCommandTests
{

    [Fact]
    void HasCorrectName()
    {
        ICommand sut = new BlockCommand();
        Assert.Equal("BLOCK", sut.Name);
    }

    [Fact]
    void CanPlaceObstacleAtPoint()
    {
        Point position = new Point(2,2);

        _tableTop.Setup(mock => mock.PlaceObstacle(position))
            .Returns(true);

        ICommand sut = new BlockCommand();

        Assert.True(sut.Execute(_writer, _tableTop.Object, _factory.Object, "2,2"));

        _tableTop.Verify(mock => mock.PlaceObstacle(position), Times.Once);
    }

}
