using webapi;
using static System.Net.Mime.MediaTypeNames;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.AddJsonFile($"appsettings.json");
        configuration.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json");
        configuration.AddEnvironmentVariables();
        configuration.AddCommandLine(args);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    }).Build();

await host.RunAsync();