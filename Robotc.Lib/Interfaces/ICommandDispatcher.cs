namespace Robotc.Lib;

public interface ICommandDispatcher 
{
    bool Dispatch(string commandString); 
    void DispatchLoop(CancellationToken? token = null);      
}