using System.Drawing;

namespace Robotc.Test;

public class MoveCommandTests : BaseCommandTests
{

    [Fact]
    public void HasCorrectName()
    {
        ICommand sut = new MoveCommand();
        Assert.Equal("MOVE", sut.Name);
    }

    [Fact]
    public void MoveCallsCalcMoveOnRobotThenValidPositonOnTableThenSetsRobotPosition()
    {
        var position = new Point(1, 0);
        using (Sequence.Create())
        {
            _tableTop.SetupGet(mock => mock.HasRobot)
                .Returns(true);

            _robot.Setup(mock => mock.CalcMove(1))
                .InSequence()
                .Returns(position);

            _tableTop.Setup(mock => mock.IsValidPosition(position))
                .InSequence()
                .Returns(true);

            _robot.SetupSet(mock => mock.Position = position)
                .InSequence();

            ICommand sut = new MoveCommand();

            Assert.True(sut.Execute(_writer, _tableTop.Object, ""));
        }
        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.CalcMove(1), Times.Once);
        _tableTop.Verify(mock => mock.IsValidPosition(position), Times.Once);
        _robot.VerifySet(mock => mock.Position = position, Times.Once);

    }

    [Fact]
    public void MoveFailsWhenNoRobot()
    {
        var position = new Point(1, 0);

        _tableTop.SetupGet(mock => mock.HasRobot)
            .Returns(false);
            
        ICommand sut = new MoveCommand();

        Assert.False(sut.Execute(_writer, _tableTop.Object, ""));

        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.CalcMove(It.IsAny<int>()), Times.Never);
        _tableTop.Verify(mock => mock.IsValidPosition(It.IsAny<Point>()), Times.Never);
        _robot.VerifySet(mock => mock.Position = It.IsAny<Point>(), Times.Never);

    }

    [Fact]
    public void MoveFailsWhenMoveIsInvalid()
    {
        var position = new Point(0, 1);
        using (Sequence.Create())
        {
            _tableTop.SetupGet(mock => mock.HasRobot)
                .Returns(true);

            _robot.Setup(mock => mock.CalcMove(1))
                .InSequence()
                .Returns(position);

            _tableTop.Setup(mock => mock.IsValidPosition(position))
                .InSequence()
                .Returns(false);

            ICommand sut = new MoveCommand();

            Assert.False(sut.Execute(_writer, _tableTop.Object, ""));
        }
        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.CalcMove(1), Times.Once);
        _tableTop.Verify(mock => mock.IsValidPosition(position), Times.Once);
        _robot.VerifySet(mock => mock.Position = position, Times.Never);

    }

}