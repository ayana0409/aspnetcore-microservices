using Ocelot.DependencyInjection;

namespace OcelotApiGw.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            //var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
            //    .Get<EventBusSettings>() ?? throw new ArgumentNullException("Invalid event bus setting");
            //services.AddSingleton<EventBusSettings>(eventBusSettings);

            return services;
        }

        public static void ConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOcelot(configuration);
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration["AllowOrigins"] 
                ?? throw new ArgumentNullException("Invalid Allow origins setting");
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}
