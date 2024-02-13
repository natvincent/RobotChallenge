namespace Robotc.Lib;

public interface ICommandDispatcher 
{
    bool Dispatch(string commandString);       
}