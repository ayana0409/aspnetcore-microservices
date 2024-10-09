namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static void AddAppConfiguration(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true, 
                        reloadOnChange: true)
                    .AddEnvironmentVariables();
        }
    }
}
