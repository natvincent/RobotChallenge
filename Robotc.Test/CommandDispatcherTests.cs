namespace Robotc.Test;

public class CommandDispatcherTests
{

    private readonly Mock<ITableTop> _tableTop = new (MockBehavior.Strict);
    private readonly Mock<ICommand> _fooCommand = new (MockBehavior.Strict);
    private readonly Mock<ICommand> _barCommand = new (MockBehavior.Strict);

    public CommandDispatcherTests()
    {
        _fooCommand.SetupGet(mock => mock.Name)
            .Returns("FOO");
        _barCommand.SetupGet(mock => mock.Name)
            .Returns("BAR");

    }

    [Fact]
    public void DispatchCallsCorrectCommandBasedOnInput()
    {
        var commandString = "BAR 1,2";
        var expectedParameters = "1,2";

        _barCommand.Setup(mock => mock.Execute(_tableTop.Object, expectedParameters))
            .Returns(true);

        ICommandDispatcher sut = new CommandDispatcher([_fooCommand.Object, _barCommand.Object], _tableTop.Object);

        Assert.True(sut.Dispatch(commandString));

        _barCommand.Verify(mock => mock.Execute(_tableTop.Object, expectedParameters), Times.Once);
        _fooCommand.Verify(mock => mock.Execute(_tableTop.Object, expectedParameters), Times.Never);
    }

    [Fact]
    public void DispatchReturnsFalseOnUnknownCommand()
    {
        var commandString = "BAZ 1,2";
        var expectedParameters = "1,2";

        ICommandDispatcher sut = new CommandDispatcher([_fooCommand.Object, _barCommand.Object], _tableTop.Object);

        Assert.False(sut.Dispatch(commandString));

        _barCommand.Verify(mock => mock.Execute(_tableTop.Object, expectedParameters), Times.Never);
        _fooCommand.Verify(mock => mock.Execute(_tableTop.Object, expectedParameters), Times.Never);
    }

}