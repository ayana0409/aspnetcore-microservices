using Infrastructure.Configurations;

namespace Ordering.API.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var emailSetting = configuration.GetSection(nameof(SmtpEmailSetting))
                .Get<SmtpEmailSetting>() ?? throw new ArgumentNullException("Invalid Email setting");

            services.AddSingleton<SmtpEmailSetting>(emailSetting);

            return services;
        }
    }
}
