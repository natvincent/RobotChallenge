using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Robotc.Lib;

public class PlaceCommand : Command
{
    private readonly Regex _regex;
    public PlaceCommand()
        : base("PLACE")
    {
        _regex = new Regex(@"(?<x>\d{1,3}),(?<y>\d{1,3}),(?<heading>WEST|NORTH|EAST|SOUTH)", RegexOptions.Compiled);

    }

    public override bool Execute(TextWriter writer, ITableTop tableTop, string parameters)
    {
        var match = _regex.Match(parameters);

        if (match.Success
            && match.Groups.Count == 4 
            && int.TryParse(match.Groups["x"].Value, out var x)
            && int.TryParse(match.Groups["y"].Value, out var y)
            && Enum.TryParse<Direction>(match.Groups["heading"].Value, true, out var heading))
        {
            var position = new Point(x, y);
            return tableTop.PlaceRobot(position, heading);
        }

        return false;
    }
}