using Infrastructure.Configurations;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.API.Application.IntegrationEvents.EventHandler;
using Shared.Configurations;
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

            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
                .Get<EventBusSettings>() ?? throw new ArgumentNullException("Invalid Event bus setting");
            services.AddSingleton(eventBusSettings);

            return services;
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOption<EventBusSettings>("EventBusSettings");
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSetting is not configure");

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                    cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutEventHandler>(ctx);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}
