using Shared.Configurations;

namespace Customer.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var hangfireSettings = configuration.GetSection(nameof(HangFireSettings))
                .Get<HangFireSettings>() ?? throw new ArgumentNullException(nameof(HangFireSettings));

            services.AddSingleton(hangfireSettings);

            return services;
        }
    }
}
