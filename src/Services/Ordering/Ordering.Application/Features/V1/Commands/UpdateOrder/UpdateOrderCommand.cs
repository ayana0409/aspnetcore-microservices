﻿using AutoMapper;
using EventBus.Messages.IntegrationEvent.Events;
using Infrastructure.Mappings;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class UpdateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<OrderDto>>, IMapfrom<Order>
        , IMapfrom<BasketCheckoutEvent>
    {
        public long Id { get; private set; }
        public void SetId(long id)
        {
            this.Id = id;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderCommand, Order>()
                .ForMember(dest => dest.Status, opts => opts.Ignore())
                .IgnoreAllNonExisting();

            profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
        }
    }
}
