﻿using Inventory.Product.API.Entities.Abstraction;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums.Inventory;

namespace Inventory.Product.API.Entities
{
    public class InventoryEntry : MongoEntity
    {
        public InventoryEntry() 
        {
            DocumentType = EDocumentType.Purchase;
            DocumentNo = Guid.NewGuid().ToString();
            ExternalDocumentNo = Guid.NewGuid().ToString();
        }
        public InventoryEntry(string id) => (Id) = id;

        [BsonElement("documentType")]
        public EDocumentType DocumentType { get; set; }

        [BsonElement("documentNo")]
        public string DocumentNo { get; set; } = string.Empty;
        [BsonElement("itemNo")]
        public string ItemNo { get; set; } = string.Empty;
        [BsonElement("quantity")]
        public int Quantity { get; set; }
        [BsonElement("externalDocumentNo")]
        public string ExternalDocumentNo { get; set; } = string.Empty;
    }
}
