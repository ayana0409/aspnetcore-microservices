namespace Shared.DTOs.Inventory
{
    public class PurchaseProductDto
    {
        public string ItemNo { get; set; } = string.Empty;
        public string DocumentNo { get; set; } = string.Empty;
        public string ExternalDocumentNo { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
