namespace Robotc.Test;

public class RightCommandTests : BaseCommandTests
{
    [Fact]
    public void NameIsCorrect()
    {
        ICommand sut = new RightCommand();
        Assert.Equal("RIGHT", sut.Name);
    }

    [Fact]
    public void CallsTurnLeftOnRobot()
    {
        using (Sequence.Create())
        {
            _tableTop.SetupGet(mock => mock.HasRobot)
                .InSequence()
                .Returns(true);

            _robot.Setup(mock => mock.Rotate(Turn.Right))
                .InSequence();

            ICommand sut = new RightCommand();

            Assert.True(sut.Execute(_writer, _tableTop.Object, ""));

        }

        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.Rotate(Turn.Right), Times.Once);
    }

    [Fact]
    public void FailsEarlyIfNoRobotPlaced()
    {
        _tableTop.SetupGet(mock => mock.HasRobot)
            .Returns(false);

        ICommand sut = new RightCommand();

        Assert.False(sut.Execute(_writer, _tableTop.Object, ""));

        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.Rotate(It.IsAny<Turn>()), Times.Never);
    }
}