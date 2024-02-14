using System.Text.RegularExpressions;

namespace Robotc.Lib;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly ITableTop _tableTop;
    private readonly TextReader _reader;
    private readonly TextWriter _writer;
    private readonly Regex _commandRegex = new (
            @"(?<command>\w*)\b\W*(?<params>.*)", 
            RegexOptions.Compiled | RegexOptions.Singleline
        );
    
    public CommandDispatcher(
            IEnumerable<ICommand> commands, 
            TextReader reader, 
            TextWriter writer,
            ITableTop tableTop
        ) 
            => (_commands, _reader, _writer, _tableTop) = (commands, reader, writer, tableTop);

    public bool Dispatch(string commandString)
    {
        var match = _commandRegex.Match(commandString);
        var commandName = match.Groups["command"].Value;
        foreach (var command in _commands) 
        {
            if (command.Name == commandName)
            {
                return command.Execute(_writer, _tableTop, match.Groups["params"].Value);
            }
        }
        return false;
    }    

    public void DispatchLoop()
    {
        while (true)
        {
            string commandString = _reader.ReadLine() ?? "";
            if (commandString.Equals("EXIT", StringComparison.InvariantCultureIgnoreCase))
                break;
            else 
                Dispatch(commandString);
        }

    }   
}