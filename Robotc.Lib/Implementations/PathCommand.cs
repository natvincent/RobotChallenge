using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.VisualBasic;

namespace Robotc.Lib;


public class PathCommand : Command
{
    private readonly Regex _regex;
    public PathCommand() : base ("PATH") 
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
            var finder = factory.CreatePathFinder(tableTop);

            if (finder.Search(tableTop.Robot.Position, new Point(x, y), out var path) 
                && path != null)
            {
                foreach (var step in path)
                    writer.WriteLine($"{step.X},{step.Y}");
                return true;
            }
        }
        return false;
    }
}
