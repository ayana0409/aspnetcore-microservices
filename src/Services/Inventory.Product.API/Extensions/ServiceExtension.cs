using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Product.API.Extensions
{
    public static class ServiceExtension
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>() 
                ?? throw new ArgumentNullException("Invalid Inventory Service DatabaseSettings");
            services.AddSingleton<DatabaseSettings>(databaseSettings);

            return services;
        }

        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOption<DatabaseSettings>(nameof(DatabaseSettings));
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                throw new ArgumentNullException("Databasesetting is not configure");

            var databaseName = settings.DatabaseName;
            var mongoDbConnectionString = settings.ConnectionString 
                + "/" + databaseName + "?authSource=admin";

            return mongoDbConnectionString;
        }

        public static void ConfigureMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(GetMongoConnectionString(services)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }

        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }
    }
}
