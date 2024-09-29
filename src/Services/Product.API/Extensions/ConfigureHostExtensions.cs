using System.Runtime.CompilerServices;

namespace Product.API.Extensions
{
    public static class ConfigureHostExtensions
    {
        public static void AddAppConfigurations(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
        }
    }
}
