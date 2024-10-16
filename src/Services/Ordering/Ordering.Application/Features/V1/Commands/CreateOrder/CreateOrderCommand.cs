using AutoMapper;
using EventBus.Messages.IntegrationEvent.Events;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<long>>, IMapfrom<Order>,
        IMapfrom<BasketCheckoutEvent>
    {
        public string UserName { get; set; } = string.Empty;
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderCommand, Order>();
            profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
        }

    }
}
