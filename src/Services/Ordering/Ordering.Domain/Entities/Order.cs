using Contracts.Common.Events;
using Ordering.Domain.Enums;
using Ordering.Domain.OrderAggregate.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{
    public class Order : AudiableEventEntity<long>
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string UserName { get; set; } = string.Empty;
        public Guid DocumentNo { get; set; } = Guid.NewGuid();
        [Column(TypeName = "Decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        [Column(TypeName = "nvarchar(250)")]
        public string EmailAddress { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string ShippingAddress { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(max)")]
        public string InvoiceAddress { get; set; } = string.Empty;

        public EOrderStatus Status { get; set; } = EOrderStatus.New;

        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName, TotalPrice, DocumentNo.ToString(), EmailAddress, ShippingAddress, InvoiceAddress));
            return this;
        }

        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }
    }
}
