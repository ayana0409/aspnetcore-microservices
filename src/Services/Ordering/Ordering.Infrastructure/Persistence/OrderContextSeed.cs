using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Persistence;
using Serilog;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderContext _context;
        public OrderContextSeed(OrderContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occured while initialising the database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occured while seeding the database");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            if (!_context.Orders.Any()) 
            {
                await _context.Orders.AddRangeAsync(
                    new Domain.Entities.Order
                    {
                        UserName = "customer1", FirstName = "customer1", LastName = "customer1",
                        EmailAddress = "customer1@local.com",
                        ShippingAddress = "Mars", InvoiceAddress = "Mars", TotalPrice = 999999
                    });
            }
        }
    }
}
