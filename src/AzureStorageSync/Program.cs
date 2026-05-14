namespace AzureStorageSync
{
    using System;
    using Catel;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Orc;
    using Serilog;
    using Serilog.Events;

    internal class Program
    {
        private static int Main(string[] args)
        {
            var settings = new HostApplicationBuilderSettings
            {
                Configuration = new ConfigurationManager()
            };

            settings.Configuration.AddEnvironmentVariables();

            var builder = Host.CreateEmptyApplicationBuilder(settings);

            // Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var services = builder.Services;

            services.AddSerilog();
            services.AddLogging();

            services.AddCatelCore();

            services.AddOrcFileSystem();

            try
            {
                HelpWriter.WriteAppHeader(s => Log.Logger.Write(LogEventLevel.Information, s));

                var context = ArgumentParser.ParseArguments(args);
                if (context.IsHelp)
                {
                    HelpWriter.WriteHelp(s => Log.Write(LogEventLevel.Information, s));

                    WaitForKeyPress();

                    return 0;
                }

                var task = Synchronizer.SyncAsync(context);
                task.Wait();

#if DEBUG
                WaitForKeyPress();
#endif

                return task.Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred");

#if DEBUG
                WaitForKeyPress();
#endif

                return -1;
            }
        }

        private static void WaitForKeyPress()
        {
            Log.Logger.Information(string.Empty);
            Log.Logger.Information("Press any key to continue");

            Console.ReadKey();
        }
    }
}
