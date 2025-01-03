using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Basket.API.Services;
using Basket.API.Services.Interfaces;
using Contracts.Common.Interfaces;
using EventBus.Messages.IntegrationEvent.Interfaces;
using Infrastructure.Common;
using Infrastructure.Extensions;
using Inventory.Grpc.Protos;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Configurations;

namespace Basket.API.Extensions
{
    public static class ServiceExtension
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
                .Get<EventBusSettings>() ?? throw new ArgumentNullException("Invalid event bus setting");
            services.AddSingleton<EventBusSettings>(eventBusSettings);

            var cacheSettings = configuration.GetSection(nameof(CacheSettings))
                .Get<CacheSettings>() ?? throw new ArgumentNullException("Invalid cache setting");
            services.AddSingleton<CacheSettings>(cacheSettings);

            var grpcSettings = configuration.GetSection(nameof(GrpcSettings))
                .Get<GrpcSettings>() ?? throw new ArgumentNullException("Invalid grpc setting");
            services.AddSingleton<GrpcSettings>(grpcSettings);

            var backgroundJobSettings = configuration.GetSection(nameof(BackgroundJobSettings))
                .Get<BackgroundJobSettings>() ?? throw new ArgumentNullException("Invalid Background Job Settings");
            services.AddSingleton<BackgroundJobSettings>(backgroundJobSettings);



            return services;
        }

        public static IServiceCollection ConfigurationService(this IServiceCollection services) =>
            services.AddScoped<IBasketRepository, BasketRepository>()
                .AddTransient<ISerializeService, SerializeService>()
                .AddTransient<IEmailTemplateService, BasketEmailTemplateService>()
            ;

        public static void ConfigureHttpClientService(this IServiceCollection services)
            => services.AddHttpClient<BackgroundJobHttpService>();

        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration) 
        {
            var settings = services.GetOption<CacheSettings>("CacheSettings");
            if (string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("Redis Connection string is not configured");

            // Redis configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = settings.ConnectionString;
            });
        }

        public static IServiceCollection CongfigureGrpcServices(this IServiceCollection services)
        {   
            var settings = services.GetOption<GrpcSettings>(nameof(GrpcSettings));
            services.AddGrpcClient<StockProtoService.StockProtoServiceClient>(x 
                => x.Address = new Uri(settings.StockUrl));
            services.AddScoped<StockItemGrpcService>();

            return services;
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOption<EventBusSettings>("EventBusSettings");
            if (string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSettings string is not configured");

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection.Host);
                });
                // Publish submit order message
                config.AddRequestClient<IBasketCheckoutEvent>();
            });
        }
    }
}
