namespace Robotc.Lib;

public interface ICommand
{
    string Name {get;}
    bool Execute(ITableTop tableTop, string parameters);
}