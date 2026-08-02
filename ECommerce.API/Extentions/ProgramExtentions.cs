using ECommerce.Domain.Contracts;

namespace ECommerce.API.Extentions
{
    public static class ProgramExtentions
    {
        public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app, CancellationToken ct = default)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");

            await seeder.SeedAsync(ct);
        }
    }
}
