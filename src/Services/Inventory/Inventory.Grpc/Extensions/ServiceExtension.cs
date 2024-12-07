using Infrastructure.Extensions;
using Inventory.Grpc.Repositories;
using Inventory.Grpc.Repositories.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Grpc.Extensions
{
    public static class ServiceExtension
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>()
                ?? throw new ArgumentNullException("Invalid Inventory Service DatabaseSettings");
            services.AddSingleton<MongoDbSettings>(databaseSettings);

            return services;
        }

        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOption<MongoDbSettings>(nameof(MongoDbSettings));
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
            services.AddScoped<IInventoryRepository, InventoryRepository>();
        }
    }
}
