using System.Drawing;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Robotc.Lib;

public class PlaceCommand : Command
{
    public PlaceCommand()
        : base("PLACE")
    {}

    public override bool Execute(ITableTop tableTop, string parameters)
    {
        var regex = new Regex(@"(\d{1,3}),(\d{1,3}),(WEST|NORTH|EAST|SOUTH)");
        
        var match = regex.Match(parameters);

        if (match.Groups.Count == 4 
            && int.TryParse(match.Groups[1].Value, out var x)
            && int.TryParse(match.Groups[2].Value, out var y)
            && Enum.TryParse<Direction>(match.Groups[3].Value, true, out var heading))
        {
            var position = new Point(x, y);
            return tableTop.PlaceRobot(position, heading);
        }

        return false;
    }
}