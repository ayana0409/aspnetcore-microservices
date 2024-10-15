namespace EventBus.Messages
{
    public interface IIntegrationEvent
    {
        public DateTime CreationDate { get; }
        public Guid Id { get; set; }
    }
}
