using AutoMapper;
using System.Reflection;

namespace Ordering.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapfrom<>);

            const string mappingMethodName = nameof(IMapfrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentType = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfor = type.GetMethod(mappingMethodName);

                if (methodInfor != null)
                {
                    methodInfor.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count <=0) continue;

                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentType);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
