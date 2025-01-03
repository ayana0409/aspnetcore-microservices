using Hangfire;
using Shared.Configurations;

namespace Customer.API.Extensions
{
    public static class HostExtensions
    {
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
