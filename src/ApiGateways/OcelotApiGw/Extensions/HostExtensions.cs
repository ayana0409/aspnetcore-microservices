using Common.Logging;
using Serilog;

namespace OcelotApiGw.Extensions
{
    public static class HostExtensions
    {
        public static void AddAppConfigurations(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(path: $"ocelot.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

            builder.Host.UseSerilog(Serilogger.Configure);
        }
    }
}
