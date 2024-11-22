using Inventory.Product.API.Entities;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Product.API.Persistence
{
    public class InventoryDbSeed
    {
        public async Task SeedDataAsync(IMongoClient mongoClient, MongoDbSettings settings)
        {
            var databaseName = settings.DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);
            var inventoryCollection = database.GetCollection<InventoryEntry>("InventoryEntries");
            if (await inventoryCollection.EstimatedDocumentCountAsync() == 0)
            {
                await inventoryCollection.InsertManyAsync(GetPreconfiguredInventoryEntries());
            }
        }

        private IEnumerable<InventoryEntry> GetPreconfiguredInventoryEntries()
        {
            return
            [
                new()
                {
                    Quantity = 10,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "Launch Vehicle",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = Shared.Enums.Inventory.EDocumentType.Purchase
                },
                new()
                {
                    Quantity = 10,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "E-Pickup",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = Shared.Enums.Inventory.EDocumentType.Purchase
                }
            ];
        }
    }
}
