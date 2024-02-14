namespace Robotc.Lib;

public class MoveCommand : Command
{
    public MoveCommand() : base ("MOVE") {}

    public override bool Execute(TextWriter writer, ITableTop tableTop, string parameters)
    {
        if (!tableTop.HasRobot) 
            return false;

        var newPosition = tableTop.Robot.CalcMove();
        if (tableTop.IsValidPosition(newPosition))
        {
            tableTop.Robot.Position = newPosition;
            return true;
        }
        return false;
    }

}