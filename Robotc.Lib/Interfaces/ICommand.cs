namespace Robotc.Lib;

public interface ICommand
{
    string Name {get;}
    bool Execute(
        TextWriter writer, 
        ITableTop tableTop, 
        IRobotFactory factory,
        string parameters
    );
}