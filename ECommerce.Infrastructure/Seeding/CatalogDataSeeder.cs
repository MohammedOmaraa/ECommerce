
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Infrastructure.Seeding
{
    public class CatalogDataSeeder(
        StoreDbContext dbContext,
        ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync(ct);
                }

                var SeedPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmptyAsync<ProductsBrand>(SeedPath, "brands.json", ct);
                await SeedIfEmptyAsync<ProductsType>(SeedPath, "types.json", ct);
                await SeedIfEmptyAsync<Product>(SeedPath, "products.json", ct);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding data.");
                throw;
            }
            
        }

        private async Task SeedIfEmptyAsync<T>(string seedPath, string fileName, CancellationToken ct) where T : class
        {
            if(await dbContext.Set<T>().AnyAsync(ct))
            {
                logger.LogInformation("Data already exists for {EntityType}. Skipping seeding.", typeof(T).Name);
                return;
            }

            var filePath = Path.Combine(seedPath, fileName);

            if (!File.Exists(filePath))
            {
                logger.LogWarning("Seed file {FileName} not found. Skipping seeding for {EntityType}.", fileName, typeof(T).Name);
                return;
            }

            await using var stream = File.OpenRead(filePath);

            var entities = await JsonSerializer.DeserializeAsync<List<T>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }, cancellationToken: ct);

            if (entities?.Count > 0)
            {
                await dbContext.Set<T>().AddRangeAsync(entities, ct);
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}
