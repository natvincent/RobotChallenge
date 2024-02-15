using Robotc;
using Robotc.Lib;

const string ExitText = "<Beep> <Boop> Goodbye.";

using var app = new App(args, Console.In, Console.Out);

Console.CancelKeyPress += delegate { };

app.Run();

if (!Console.IsOutputRedirected)
    Console.WriteLine(ExitText);