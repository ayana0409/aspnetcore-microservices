﻿using Contracts.Common.Interfaces;
using Contracts.Identity;
using Infrastructure.Common;
using Infrastructure.Extensions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Conventions;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;
using Product.API.Repositories;
using Product.API.Repositories.Interfaces;
using Shared.Configurations;
using System.Text;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services
            ,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings))
                .Get<JwtSettings>() ?? throw new ArgumentNullException("Invalid Jwt Settings");
            services.AddSingleton(jwtSettings);

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => options.LowercaseQueryStrings = true);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureProductDbContext(configuration);
            services.AddInfrastructureServices();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
            services.AddJwtAuthentication();

            return services;
        }

        internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var settings = services.GetOption<JwtSettings>(nameof(JwtSettings));
            if (settings == null || string.IsNullOrEmpty(settings.Key))
                throw new ArgumentNullException($"{nameof(JwtSettings)} is not configured properly");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.UseSecurityTokenValidators = true;
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParameter;
            });

            return services;
        }

        private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString") ?? throw new ApplicationException("Invalid connection string");
            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<ProductContext>(m => m.UseMySql(builder.ConnectionString,
                ServerVersion.AutoDetect(builder.ConnectionString), e => 
            {
                    e.MigrationsAssembly("Product.API");
                    e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
            }));

            return services;
        }

        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services
                    .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBase<,,>))
                    .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                    .AddScoped<IProductRepository, ProductRepository>()
                ;
        }
    }
}
