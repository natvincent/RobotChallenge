using System.Drawing;

namespace Robotc.Test;

public class PathCommandTests : BaseCommandTests
{
    [Fact]
    public void PathCommandNameIsCorrect()
    {
        ICommand sut = new PathCommand();
        Assert.Equal("PATH", sut.Name);
    }

    [Fact]
    public void CallsPathFinderWithCorrectParams()
    {
        ICommand sut = new PathCommand();

        var fromPosition = new Point(0, 0);
        var toPosition = new Point(0, 1);

        IEnumerable<Point>? path = new Point[]{new (0, 0), new (0, 1)};

        Mock<IPathFinder> finder = new Mock<IPathFinder>(MockBehavior.Strict);
        finder.Setup(mock => mock.Search(fromPosition, toPosition, out path))
            .Returns(true);

        _factory.Setup(mock => mock.CreatePathFinder(It.IsAny<ITableTop>()))
            .Returns(finder.Object);
        _robot.SetupGet(mock => mock.Position)
            .Returns(fromPosition);

        sut.Execute(_writer, _tableTop.Object, _factory.Object, "0,1");

        Assert.Equal(
              "0,0" + _writer.NewLine
            + "0,1" + _writer.NewLine,
            _writer.ToString()
        );

        _robot.VerifyGet(mock => mock.Position, Times.Once);
    }

}