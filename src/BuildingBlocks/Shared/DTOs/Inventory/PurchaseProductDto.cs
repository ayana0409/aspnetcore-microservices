using Shared.Enums.Inventory;

namespace Shared.DTOs.Inventory
{
    public class PurchaseProductDto
    {
        public EDocumentType DocumentType { get; set; } = EDocumentType.Purchase;
        public string ItemNo { get; set; } = string.Empty;
        public string DocumentNo { get; set; } = string.Empty;
        public string ExternalDocumentNo { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
