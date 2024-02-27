using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robotc.Lib;

namespace Robotc.Lib;

public class App : IDisposable
{
    private readonly IHost _host;
    private readonly IServiceScope _scope;
    private readonly ICommandDispatcher _dispatcher;

    public App(string[] args, TextReader consoleIn, TextWriter consoleOut)
    {
        _host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => 
            {
                services.AddSingleton<TextReader>(consoleIn);
                services.AddSingleton<TextWriter>(consoleOut);

                services.AddScoped<ICommandDispatcher, CommandDispatcher>();
                services.AddScoped<ITableTop, TableTop>();
                services.AddScoped<IRobot, Robot>();
                services.AddScoped<IRobotFactory, RobotFactory>();

                services.AddScoped<ICommand, PlaceCommand>();
                services.AddScoped<ICommand, MoveCommand>();
                services.AddScoped<ICommand, LeftCommand>();
                services.AddScoped<ICommand, RightCommand>();
                services.AddScoped<ICommand, ReportCommand>();
                services.AddScoped<ICommand, BlockCommand>();

            })
            .Build();
        _scope = _host.Services.CreateScope();
        _dispatcher = _scope.ServiceProvider
            .GetRequiredService<ICommandDispatcher>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _host.Dispose();
    }

    public void Run() => _dispatcher.DispatchLoop();

}