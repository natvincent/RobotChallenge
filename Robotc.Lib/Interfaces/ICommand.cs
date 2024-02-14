namespace Robotc.Lib;

public interface ICommand
{
    string Name {get;}
    bool Execute(TextWriter writer, ITableTop tableTop, string parameters);
}