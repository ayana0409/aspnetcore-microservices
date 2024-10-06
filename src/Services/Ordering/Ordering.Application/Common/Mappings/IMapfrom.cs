using AutoMapper;

namespace Ordering.Application.Common.Mappings
{
    public interface IMapfrom<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
