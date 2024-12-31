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

            return services;    
        }
    }
}
