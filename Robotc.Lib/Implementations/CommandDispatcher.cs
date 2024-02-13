using System.Text.RegularExpressions;

namespace Robotc.Lib;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IEnumerable<ICommand> _commands;
    private readonly ITableTop _tableTop;
    private readonly Regex _commandRegex = new (
            @"(?<command>\w*)\b\W*(?<params>.*)", 
            RegexOptions.Compiled | RegexOptions.Singleline
        );
    
    public CommandDispatcher(IEnumerable<ICommand> commands, ITableTop tableTop) 
        => (_commands, _tableTop) = (commands, tableTop);

    public bool Dispatch(string commandString)
    {
        var match = _commandRegex.Match(commandString);
        var commandName = match.Groups["command"].Value;
        foreach (var command in _commands) 
        {
            if (command.Name == commandName)
            {
                return command.Execute(_tableTop, match.Groups["params"].Value);
            }
        }
        return false;
    }       
}