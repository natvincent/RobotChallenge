using System.Drawing;

namespace Robotc.Test;

public class ReportCommandTests : BaseCommandTests
{
    [Fact]
    public void HasCorrectName()
    {
        ICommand sut = new ReportCommand();
        Assert.Equal("REPORT", sut.Name);
    }

    [Fact]
    public void ReportOutputsRobotPositionAndHeading()
    {
        _tableTop.SetupGet(mock => mock.HasRobot)
            .Returns(true);
        _robot.SetupGet(mock => mock.Position)
            .Returns(new Point(2, 2));
        _robot.SetupGet(mock => mock.Heading)
            .Returns(Direction.South);
        
        ICommand sut = new ReportCommand();

        Assert.True(sut.Execute(_writer, _tableTop.Object, _factory.Object, ""));

        Assert.Equal("2,2,SOUTH" + _writer.NewLine, _writer.ToString());
        _robot.VerifyGet(mock => mock.Position, Times.Once);
        _robot.VerifyGet(mock => mock.Heading, Times.Once);
    }

    [Fact]
    public void ReportDoesntReportTillRobotIsPlaced()
    {
        _tableTop.SetupGet(mock => mock.HasRobot)
            .Returns(false);
        
        ICommand sut = new ReportCommand();

        Assert.False(sut.Execute(_writer, _tableTop.Object, _factory.Object, ""));

        _tableTop.VerifyGet(mock => mock.Robot, Times.Never);
        _robot.VerifyGet(mock => mock.Position, Times.Never);
        _robot.VerifyGet(mock => mock.Heading, Times.Never);

    }
}