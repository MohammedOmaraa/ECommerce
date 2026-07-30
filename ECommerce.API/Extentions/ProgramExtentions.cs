using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Extentions
{
    public static class ProgramExtentions
    {
        public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app, CancellationToken ct = default)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreDbContext>();
            var catalogLogger = services.GetRequiredService<ILogger<CatalogDataSeeder>>();
            
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync(ct);
            }

            CatalogDataSeeder catalogSeeder = new CatalogDataSeeder(dbContext, catalogLogger);

            await catalogSeeder.SeedAsync(ct);
        }
    }
}
