using System.Transactions;

namespace Robotc.Lib;

public class RightCommand : RotateCommand
{
    public RightCommand() : base("RIGHT", Turn.Right) { }

}