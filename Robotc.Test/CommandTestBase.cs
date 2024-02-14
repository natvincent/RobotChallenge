namespace Robotc.Test;

public class BaseCommandTests 
{
    protected readonly Mock<ITableTop> _tableTop = new (MockBehavior.Strict);
    protected readonly Mock<IRobot> _robot = new (MockBehavior.Strict);

    protected readonly StringWriter _writer = new ();

    public BaseCommandTests()
    {
        _tableTop.SetupGet(mock => mock.Robot)
            .Returns(_robot.Object);
    }

}
