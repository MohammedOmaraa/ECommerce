
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductsBrand> ProductsBrands { get; set; }

        public DbSet<ProductsType> ProductsTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
