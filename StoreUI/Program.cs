using Serilog;
//using Microsoft.Extensions.Logging;

using UI;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"..\StoreDL\logger.txt")
    .CreateLogger();

MainMenu menu = new MainMenu();
menu.Start();