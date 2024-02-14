using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robotc;
using Robotc.Lib;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => 
    {
        services.AddSingleton<TextReader>(Console.In);
        services.AddSingleton<TextWriter>(Console.Out);

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<ITableTop, TableTop>();
        services.AddScoped<IRobot, Robot>();
        services.AddScoped<IRobotFactory, RobotFactory>();

        services.AddScoped<ICommand, PlaceCommand>();
        services.AddScoped<ICommand, MoveCommand>();
        services.AddScoped<ICommand, LeftCommand>();
        services.AddScoped<ICommand, RightCommand>();
        services.AddScoped<ICommand, ReportCommand>();

    })
    .Build();

using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

services.GetRequiredService<ICommandDispatcher>().DispatchLoop();

