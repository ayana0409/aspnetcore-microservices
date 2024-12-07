using Infrastructure.Common;
using Inventory.Grpc.Entities;
using Inventory.Grpc.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shared.Configurations;

namespace Inventory.Grpc.Repositories
{
    public class InventoryRepository : MongoDbRepository<InventoryEntry>, IInventoryRepository
    {
        public InventoryRepository(IMongoClient client, MongoDbSettings settings) : base(client, settings)
        {
        }

        public async Task<int> GetStockQuantity(string itemNo)
            => await Collection.AsQueryable()
            .Where(x => x.ItemNo.Equals(itemNo))
            .SumAsync(x => x.Quantity);
    }
}
