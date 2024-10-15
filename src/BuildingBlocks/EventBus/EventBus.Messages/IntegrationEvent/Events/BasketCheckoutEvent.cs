using EventBus.Messages.IntegrationEvent.Interfaces;

namespace EventBus.Messages.IntegrationEvent.Events
{
    public record BasketCheckoutEvent() : IntegrationBaseEvent, IBasketCheckoutEvent
    {
        public string UserName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string InvoiceAddress { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
    }
}
