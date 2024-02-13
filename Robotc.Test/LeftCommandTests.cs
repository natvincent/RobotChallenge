namespace Robotc.Test;

public class LeftCommandTests : BaseCommandTests
{
    [Fact]
    public void NameIsCorrect()
    {
        ICommand sut = new LeftCommand();
        Assert.Equal("LEFT", sut.Name);
    }

    [Fact]
    public void CallsTurnLeftOnRobot()
    {
        using (Sequence.Create())
        {
            _tableTop.SetupGet(mock => mock.HasRobot)
                .InSequence()
                .Returns(true);

            _robot.Setup(mock => mock.Rotate(Turn.Left))
                .InSequence();

            ICommand sut = new LeftCommand();

            Assert.True(sut.Execute(_tableTop.Object, ""));

        }

        _tableTop.VerifyGet(mock => mock.HasRobot, Times.Once);
        _robot.Verify(mock => mock.Rotate(Turn.Left), Times.Once);
    }
}