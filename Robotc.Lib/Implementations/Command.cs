namespace Robotc.Lib;

public abstract class Command : ICommand 
{
    public Command(string name) 
        => Name = name;

    public abstract bool Execute(ITableTop tableTop, string parameters);

    public string Name { get; }
}