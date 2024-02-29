using System.ComponentModel;
using System.Drawing;
using System.Formats.Asn1;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.VisualBasic;

namespace Robotc.Lib;


public class PathCommand : Command
{
    private readonly Regex _regex;
    public PathCommand() : base ("PATH") 
    {
        _regex = new Regex(@"(?<x>\d{1,3}),(?<y>\d{1,3})(,(?<points>POINTS)){0,1}", RegexOptions.Compiled);
    }

    private void OutputPoints(TextWriter writer, IEnumerable<Point>? path)
    {
        if (path == null)
            return;

        foreach (var step in path)
            writer.WriteLine($"{step.X},{step.Y}");
    }


    private void OutputWalk(ITableTop tableTop, TextWriter writer, IEnumerable<Point>? path)
    {
        if (path == null)
            return;

        var enumerator = path.GetEnumerator();
        if (enumerator.MoveNext())
        {
            var pawn = tableTop.Robot.Clone();
            while (enumerator.MoveNext())
            {
                var step = enumerator.Current;
                if (pawn.RotateTowards(step, out var turn, out var count))
                {
                    while (count > 0)
                    {
                        writer.WriteLine(turn.ToString().ToUpper());
                        count--;
                    }
                    pawn.Position = step;
                    writer.WriteLine(@"MOVE");
                }
            }
        }
    }

    public override bool Execute(TextWriter writer, ITableTop tableTop, IRobotFactory factory, string parameters)
    {
        var match = _regex.Match(parameters);

        if (match.Success
            && match.Groups.Count >= 3 
            && int.TryParse(match.Groups["x"].Value, out var x)
            && int.TryParse(match.Groups["y"].Value, out var y))
        {
            var finder = factory.CreatePathFinder(tableTop);

            if (finder.Search(tableTop.Robot.Position, new Point(x, y), out var path) 
                && path != null)
            {
                if (match.Groups.Count > 3 
                    && match.Groups["points"].Value == "POINTS")
                    OutputPoints(writer, path);
                else
                    OutputWalk(tableTop, writer, path);
            }
        }
        return false;
    }
}
