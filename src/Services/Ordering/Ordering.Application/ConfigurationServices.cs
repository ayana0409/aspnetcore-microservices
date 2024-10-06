using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Behavious;
using System.Reflection;

namespace Ordering.Application
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(config =>
                    {
                        config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    })
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            ;
    }
}
