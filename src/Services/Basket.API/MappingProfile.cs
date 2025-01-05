using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.IntegrationEvent.Events;
using Shared.DTOs.Basket;

namespace Basket.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
