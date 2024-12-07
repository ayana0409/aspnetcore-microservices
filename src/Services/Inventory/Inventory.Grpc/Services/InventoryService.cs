using Grpc.Core;
using Inventory.Grpc.Protos;
using Inventory.Grpc.Repositories.Interfaces;

namespace Inventory.Grpc.Services;

public class InventoryService : StockProtoService.StockProtoServiceBase
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly Serilog.ILogger _logger;

    public InventoryService(Serilog.ILogger logger, IInventoryRepository inventoryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
    }

    public override async Task<StockModel> GetStock(GetStockRequest request, ServerCallContext context)
    {
        _logger.Information($"BEGIN Get Stock of ItemNo: {request.ItemNo}");

        var stockQuantity = await _inventoryRepository.GetStockQuantity(request.ItemNo);
        var result = new StockModel()
        {
            Quantity = stockQuantity
        };

        _logger.Information($"END Get Stock of ItemNo: {request.ItemNo} - Quantity: {result.Quantity}");
        return result;
    }
}
