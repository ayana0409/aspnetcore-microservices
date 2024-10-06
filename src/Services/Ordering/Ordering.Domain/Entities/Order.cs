using Contracts.Domains;
using Ordering.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditBase<long>
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string UserName { get; set; } = string.Empty;
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

        public EOrderStatus Status { get; set; }
    }
}
