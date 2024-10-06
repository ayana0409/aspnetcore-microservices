using Ordering.Application.Common.Mappings;
using Ordering.Domain.Entities;
using Ordering.Domain.Enums;

namespace Ordering.Application.Common.Models
{
    public class OrderDto : IMapfrom<Order>
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        // Address
        public string ShippingAddress { get; set; } = string.Empty;
        public string InvoiceAddress { get; set; } = string.Empty;

        public EOrderStatus Status { get; set; }
    }
}
