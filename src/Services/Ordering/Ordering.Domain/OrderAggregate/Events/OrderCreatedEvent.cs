using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public long Id { get; private set; }
        public string UserName { get; set; } = string.Empty;

        public string DocumentNo { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string InvoiceAddress { get; set; } = string.Empty;

        public OrderCreatedEvent(long id, string userName, decimal totalPrice, string documentNo, string emailAddress, string shippingAddress, string invoiceAddress)
        {
            Id = id;
            UserName = userName;
            DocumentNo = documentNo;
            TotalPrice = totalPrice;
            EmailAddress = emailAddress;
            ShippingAddress = shippingAddress;
            InvoiceAddress = invoiceAddress;
        }
    }
}
