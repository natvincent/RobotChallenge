namespace Robotc.Lib;

public abstract class Command : ICommand 
{
    public Command(string name) 
        => Name = name;

    public string Name { get; }

    public abstract bool Execute(
        TextWriter writer, 
        ITableTop tableTop, 
        IRobotFactory factory,
        string parameters
    );

}