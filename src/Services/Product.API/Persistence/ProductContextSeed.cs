using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(GetCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContext));
            }
        }

        private static IEnumerable<CatalogProduct> GetCatalogProducts()
        {
            return
            [
                new()
                {
                    No = "E-Pickup",
                    Name = "CyberTruck",
                    Summary = "Created for Mars",
                    Description = "Cover by steel on Starship",
                    Price = (decimal)19345.39,
                },
                new()
                {
                    No = "Launch Vehicle",
                    Name = "Starship",
                    Summary = "Lading on Mars",
                    Description = "The most powerful launch vehicle in the word",
                    Price = (decimal)69999923.39,
                }
            ];
        }
    }
}
