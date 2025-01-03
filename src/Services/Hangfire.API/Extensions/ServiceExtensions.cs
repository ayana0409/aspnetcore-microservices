using Contracts.ScheduledJobs;
using Contracts.Services;
using Hangfire.API.Services.Interfaces;
using Hangfire.API.Services;
using Infrastructure.Configurations;
using Infrastructure.ScheduledJobs;
using Infrastructure.Services;
using Shared.Configurations;

namespace Hangfire.API.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var hangfireSettings = configuration.GetSection(nameof(HangFireSettings))
                .Get<HangFireSettings>() ?? throw new ArgumentNullException(nameof(HangFireSettings));
            services.AddSingleton(hangfireSettings);

            var emailSettings = configuration.GetSection(nameof(SmtpEmailSetting))
                .Get<SmtpEmailSetting>() ?? throw new ArgumentNullException(nameof(SmtpEmailSetting));
            services.AddSingleton(emailSettings);

            return services;    
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
            => services.AddTransient<IScheduledJobService, HangfireService>()
                .AddScoped<ISmtpEmailService, SmtpEmailService>()
                .AddTransient<IBackgroundJobService, BackgroundJobService>()
            ;
    }
}
