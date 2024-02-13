using System.Transactions;

namespace Robotc.Lib;

public class LeftCommand : Command
{
    public LeftCommand() : base("LEFT") { }

    public override bool Execute(ITableTop tableTop, string parameters)
    {
        if (!tableTop.HasRobot)
            return false;

        tableTop.Robot.TurnLeft();
        return true;
    }
}