
using System.Xml.XPath;

namespace Robotc.Lib;

public class ReportCommand : Command
{
    public ReportCommand() : base ("REPORT") { }

    public override bool Execute(TextWriter writer, ITableTop tableTop, string parameters)
    {
        if (!tableTop.HasRobot)
          return false;
          
        var robot = tableTop.Robot;
        var position = robot.Position;
        var heading = robot.Heading;
        
        writer.WriteLine($"{position.X},{position.Y},{heading.ToString().ToUpper()}");
        return true;
    }
}