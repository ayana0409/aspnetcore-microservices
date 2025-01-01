using Shared.Configurations;

namespace Hangfire.API.Extensions
{
    public static class HostExtensions
    {
        public static void AddAppConfigurations(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        internal static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
        {
            var configureDashboard = configuration.GetSection("HangFireSettings:Dashboard").Get<DashboardOptions>() 
                ?? throw new ArgumentNullException(nameof(DashboardOptions));
            var hangfireSettings = configuration.GetSection("HangFireSettings").Get<HangFireSettings>()
                ?? throw new ArgumentNullException(nameof(HangFireSettings));
            var hangfireRoute = hangfireSettings.Route;

            app.UseHangfireDashboard(hangfireRoute, new DashboardOptions()
            {
                //Authorization = new[] {},
                DashboardTitle = configureDashboard.DashboardTitle,
                StatsPollingInterval = configureDashboard.StatsPollingInterval,
                AppPath = configureDashboard.AppPath,
                IgnoreAntiforgeryToken = true
            });

            return app;
        }
    }
}
