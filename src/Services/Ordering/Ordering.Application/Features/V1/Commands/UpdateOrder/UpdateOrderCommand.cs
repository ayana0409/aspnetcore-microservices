using AutoMapper;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class UpdateOrderCommand : IRequest<ApiResult<OrderDto>>, IMapfrom<Order>
    {
        public long Id { get; private set; }
        public void SetId(long id)
        {
            this.Id = id;
        }

        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string ShippingAddress { get; set; } = string.Empty;
        public string InvoiceAddress { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderCommand, Order>();
        }
    }
}
