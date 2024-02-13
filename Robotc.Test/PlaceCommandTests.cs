using System.Drawing;
using Xunit.Sdk;

namespace Robotc.Test;

public class CommandTests 
{
    private readonly Mock<ITableTop> _tableTop = new (MockBehavior.Strict);

    [Fact]
    public void PlaceCommandNameIsCorrect()
    {
        ICommand sut = new PlaceCommand();
        Assert.Equal("PLACE", sut.Name);
    }

    [Fact]
    public void PlaceCommandCallsPlaceRobotOnTableTop()
    {
        var expectedPosition = new Point(1, 1);

        _tableTop.Setup(mock => mock.PlaceRobot(expectedPosition, Direction.North))
            .Returns(true);

        ICommand sut = new PlaceCommand();

        sut.Execute(
            _tableTop.Object, 
            "1,1,NORTH"
        );

        _tableTop.Verify(mock => mock.PlaceRobot(expectedPosition, Direction.North), Times.Once);

    }

    [Fact]
    public void PlaceCommandFailsWhenPositionIsInvalid()
    {
        var expectedPosition = new Point(3, 3);

        _tableTop.Setup(mock => mock.PlaceRobot(expectedPosition, Direction.North))
            .Returns(false);

        ICommand sut = new PlaceCommand();

        Assert.False(sut.Execute(
            _tableTop.Object, 
            "3,3,NORTH"
        ));

        _tableTop.Verify(mock => mock.PlaceRobot(expectedPosition, Direction.North), Times.Once);
    }

    [Fact]
    public void PlaceCommandFailsWhenHeadingIsInvalid()
    {
        ICommand sut = new PlaceCommand();

        Assert.False(sut.Execute(
            _tableTop.Object, 
            "3,3,SIDEWAYS"
        ));

        _tableTop.Verify(mock => mock.PlaceRobot(It.IsAny<Point>(), It.IsAny<Direction>()), Times.Never);
    }


}