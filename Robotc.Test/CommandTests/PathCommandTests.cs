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
    public void CanOutputPathOneSquareAway()
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
        _robot.SetupGet(mock => mock.Heading)
            .Returns(Direction.West);

        Mock<IRobot> pawn = new Mock<IRobot>(MockBehavior.Strict);

        _robot.Setup(mock => mock.Clone())
            // .InSequence()
            .Returns(pawn.Object);

        using (Sequence.Create())
        {
            var count = 2;
            var turn = Turn.Right;
            pawn.Setup(mock => mock.RotateTowards(new Point(0, 1), out turn, out count))
                .InSequence()
                .Returns(true);

            pawn.SetupSet(mock => mock.Position = new Point(0, 1))
                .InSequence();

            sut.Execute(_writer, _tableTop.Object, _factory.Object, "0,1");

            Assert.Equal(
                "RIGHT" + _writer.NewLine
                + "RIGHT" + _writer.NewLine
                + "MOVE" + _writer.NewLine,
                _writer.ToString()
            );
        }

    }

    [Fact]
    public void CanOutputPathTwoSquaresAway()
    {
        ICommand sut = new PathCommand();

        var fromPosition = new Point(0, 0);
        var toPosition = new Point(1, 1);

        IEnumerable<Point>? path = new Point[]{new (0, 0), new (0, 1), new (1, 1)};

        Mock<IPathFinder> finder = new(MockBehavior.Strict);
        finder.Setup(mock => mock.Search(fromPosition, toPosition, out path))
            .Returns(true);

        _factory.Setup(mock => mock.CreatePathFinder(It.IsAny<ITableTop>()))
            .Returns(finder.Object);

        _robot.SetupGet(mock => mock.Position)
            .Returns(fromPosition);
        _robot.SetupGet(mock => mock.Heading)
            .Returns(Direction.West);

        Mock<IRobot> pawn = new Mock<IRobot>(MockBehavior.Strict);

        _robot.Setup(mock => mock.Clone())
            // .InSequence()
            .Returns(pawn.Object);

        using (Sequence.Create())
        {
            var count = 2;
            var turn = Turn.Right;
            pawn.Setup(mock => mock.RotateTowards(new Point(0, 1), out turn, out count))
                .InSequence()
                .Returns(true);

            pawn.SetupSet(mock => mock.Position = new Point(0, 1))
                .InSequence();

            count = 1;
            turn = Turn.Right;

            pawn.Setup(mock => mock.RotateTowards(new Point(1, 1), out turn, out count))
                .InSequence()
                .Returns(true);

            pawn.SetupSet(mock => mock.Position = new Point(1, 1))
                .InSequence();

            sut.Execute(_writer, _tableTop.Object, _factory.Object, "1,1");

            Assert.Equal(
                "RIGHT" + _writer.NewLine
                + "RIGHT" + _writer.NewLine
                + "MOVE" + _writer.NewLine
                + "RIGHT" + _writer.NewLine
                + "MOVE" + _writer.NewLine,
                _writer.ToString()
            );
        }
    }

    [Fact]
    public void PathWithPointsParamOutputsStepCoordinates()
    {
        ICommand sut = new PathCommand();

        var fromPosition = new Point(0, 0);
        var toPosition = new Point(1, 1);

        IEnumerable<Point>? path = new Point[]{new (0, 0), new (0, 1), new (1, 1)};

        Mock<IPathFinder> finder = new(MockBehavior.Strict);
        finder.Setup(mock => mock.Search(fromPosition, toPosition, out path))
            .Returns(true);

        _factory.Setup(mock => mock.CreatePathFinder(It.IsAny<ITableTop>()))
            .Returns(finder.Object);

        _robot.SetupGet(mock => mock.Position)
            .Returns(fromPosition);
        _robot.SetupGet(mock => mock.Heading)
            .Returns(Direction.West);

        Mock<IRobot> pawn = new Mock<IRobot>(MockBehavior.Strict);

        sut.Execute(_writer, _tableTop.Object, _factory.Object, "1,1,POINTS");

        Assert.Equal(
            "0,0" + _writer.NewLine
            + "0,1" + _writer.NewLine
            + "1,1" + _writer.NewLine,
            _writer.ToString()
        );
    }
}