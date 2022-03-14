using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Reflection;

namespace TodoApi.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder, Action<HostBuilderContext, LoggerConfiguration> configureLogger = null)
        {
            hostBuilder.UseSerilog(((context, configuration) =>
            {
                var assembly = Assembly.GetEntryAssembly()?.GetName().Name;

                configuration.ReadFrom.Configuration(context.Configuration, "Logging")
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", assembly)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment);

                var logFileName = $"logs/{assembly}";

                configuration.WriteTo.File($"{logFileName}.log");
                // Separate log file for errors.
                configuration.WriteTo.File($"{logFileName}.Errors.log", LogEventLevel.Error);

                configureLogger?.Invoke(context, configuration);

                if (context.HostingEnvironment.IsDevelopment())
                {
                    configuration.WriteTo.Console();
                }
            }));

            return hostBuilder;
        }
    }
}