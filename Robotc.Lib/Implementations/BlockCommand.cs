using System.Drawing;
using System.Text.RegularExpressions;

namespace Robotc.Lib;

public class BlockCommand : Command
{
    private readonly Regex _regex;
    public BlockCommand() : base("BLOCK") 
    { 
        _regex = new Regex(@"(?<x>\d{1,3}),(?<y>\d{1,3})", RegexOptions.Compiled);
    }

    public override bool Execute(TextWriter writer, ITableTop tableTop, IRobotFactory factory, string parameters)
    {
        var match = _regex.Match(parameters);

        if (match.Success
            && match.Groups.Count == 3 
            && int.TryParse(match.Groups["x"].Value, out var x)
            && int.TryParse(match.Groups["y"].Value, out var y))
        {
            return tableTop.PlaceObstacle(new Point(x, y));
        }

        return false;
    }
}