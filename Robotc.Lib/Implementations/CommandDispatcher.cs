using System.Text.RegularExpressions;

namespace Robotc.Lib;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly ITableTop _tableTop;
    private readonly TextReader _reader;
    private readonly TextWriter _writer;
    private readonly IRobotFactory _factory;
    private readonly Regex _commandRegex;
    //  = new (
    //         @"(?<command>\w*)\b\W*(?<params>.*)", 
    //         RegexOptions.Compiled | RegexOptions.Singleline
    //     );
    
    public CommandDispatcher(
            IEnumerable<ICommand> commands, 
            TextReader reader, 
            TextWriter writer,
            ITableTop tableTop,
            IRobotFactory factory
        ) 
    {
        (_commands, _reader, _writer, _tableTop, _factory) = (commands, reader, writer, tableTop, factory);
        var commandPart = String.Join('|', commands.Select(cmd => cmd.Name).ToArray());
        _commandRegex = new Regex($"(?<command>{commandPart})\\b\\W*(?<params>.*)");
    }
            

    public bool Dispatch(string commandString)
    {
        if (String.IsNullOrEmpty(commandString)) 
            return false;
            
        var match = _commandRegex.Match(commandString);
        var commandName = match.Groups["command"].Value;
        foreach (var command in _commands) 
        {
            if (command.Name == commandName)
            {
                return command.Execute(_writer, _tableTop, _factory, match.Groups["params"].Value);
            }
        }
        return false;
    }    

    public async void DispatchLoop(CancellationToken? token = null)
    {
        CancellationToken realToken = token ?? new CancellationToken();

        while (!realToken.IsCancellationRequested)
        {
            string? commandString = await _reader.ReadLineAsync(realToken);
            if (commandString == null || commandString.Equals("EXIT", StringComparison.InvariantCultureIgnoreCase))
                break;
            else 
                Dispatch(commandString);
        }

    }   
}