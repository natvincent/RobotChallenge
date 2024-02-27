using System.Transactions;

namespace Robotc.Lib;

public class RotateCommand : Command
{
    private readonly Turn _turn;

    public RotateCommand(string name, Turn turn) : base(name) => _turn = turn;

    public override bool Execute(TextWriter writer, ITableTop tableTop, IRobotFactory factory, string parameters)
    {
        if (!tableTop.HasRobot)
            return false;

        tableTop.Robot.Rotate(_turn);

        return true;
    }
}