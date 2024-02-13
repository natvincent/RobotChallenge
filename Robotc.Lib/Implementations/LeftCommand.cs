using System.Transactions;

namespace Robotc.Lib;

public class LeftCommand : RotateCommand
{
    public LeftCommand() : base("LEFT", Turn.Left) { }

}