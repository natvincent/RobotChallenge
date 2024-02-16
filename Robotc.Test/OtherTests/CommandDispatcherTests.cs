using System.Threading.Tasks.Sources;

namespace Robotc.Test;

public class CommandDispatcherTests
{

    private readonly Mock<ITableTop> _tableTop = new (MockBehavior.Strict);
    private readonly Mock<ICommand> _fooCommand = new (MockBehavior.Strict);
    private readonly Mock<ICommand> _barCommand = new (MockBehavior.Strict);
    private readonly StringWriter _writer = new ();

    private IEnumerable<ICommand> Commands { get => [_fooCommand.Object, _barCommand.Object]; }

    private void ClearOutInvocations() 
    {
        _fooCommand.Invocations.Clear();
        _barCommand.Invocations.Clear();
    }

    private ICommandDispatcher CreateDispatcher(string commandList = "") 
    {
        var dispatcher = new CommandDispatcher(Commands, new StringReader(commandList), _writer, _tableTop.Object);

        ClearOutInvocations();

        return dispatcher;
    }

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

        _barCommand.Setup(mock => mock.Execute(_writer, _tableTop.Object, expectedParameters))
            .Returns(true);

        var sut = CreateDispatcher();

        Assert.True(sut.Dispatch(commandString));

        _barCommand.Verify(mock => mock.Execute(_writer, _tableTop.Object, expectedParameters), Times.Once);
        _fooCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void DispatchReturnsFalseOnUnknownCommand()
    {
        var commandString = "BAZ 1,2";

        var sut = CreateDispatcher();

        Assert.False(sut.Dispatch(commandString));

        _barCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
        _fooCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void DispatchHandlesReallyLongCommandAndArguments()
    {
        var commandString = "BAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZBAZ 111111111111111111111111111111111111,222222222222222222222222222222222222";

        var sut = CreateDispatcher();

        Assert.False(sut.Dispatch(commandString));

        _barCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
        _fooCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
    }

   [Fact]
    public void DispatchExitsWithExitCommand()
    {
        var sut = CreateDispatcher("EXIT\n");

        sut.DispatchLoop();

        _fooCommand.VerifyGet(mock => mock.Name, Times.Never);
        _barCommand.VerifyGet(mock => mock.Name, Times.Never);
    }

    [Fact]
    public void DispatchFromTextReader()
    {
        _fooCommand.Setup(mock => mock.Execute(_writer, _tableTop.Object, ""))
            .Returns(true);

        var sut = CreateDispatcher("FOO\nEXIT\n");

        sut.DispatchLoop();

        _fooCommand.Verify(mock => mock.Execute(_writer, _tableTop.Object, ""), Times.Once);
        _barCommand.Verify(mock => mock.Execute(It.IsAny<TextWriter>(), It.IsAny<ITableTop>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async void DispatchLoopExitsWhenReachingEndOfFile()
    {

        const int TimeoutMS = 200;

        var tokenSource = new CancellationTokenSource(TimeoutMS);
        tokenSource.Token.ThrowIfCancellationRequested();

        var task = Task.Run(() =>
            {

                _fooCommand.Setup(mock => mock.Execute(_writer, _tableTop.Object, ""))
                    .Returns(true);

                var sut = CreateDispatcher("FOO\n");

                sut.DispatchLoop(tokenSource.Token);

                _fooCommand.Verify(mock => mock.Execute(_writer, _tableTop.Object, ""), Times.Once);
            },
            tokenSource.Token
        );

        await task;

        Assert.False(tokenSource.IsCancellationRequested, "DispatchLoop() did not exit at end of stream");

    }

}