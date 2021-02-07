using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Application Starting Up");
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var appConfig = services.GetRequiredService<IConfiguration>();
                        string logFilePath = appConfig.GetValue<string>("Serilog:path");
                        string logLevel = appConfig.GetValue<string>("Serilog:LogLevel");
                        Log.Logger = new LoggerConfiguration()
                                        .Enrich.FromLogContext()
                                        .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information))
                                        .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                                        .CreateLogger();
                        Serilog.Debugging.SelfLog.Enable(ex => Log.Warning(ex));
                        Log.Information("<<<< HAHN Application service is configured and running >>>>");
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while preparing the required services.");
                        throw;
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }
        //CreateHostBuilder(args).Build().Run();
    

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
