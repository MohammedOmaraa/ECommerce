
using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.OwnsOne(o => o.Product, product =>
            {
                product.Property(p => p.ProductId)
                    .IsRequired();
                product.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);
                product.Property(p => p.PictureUrl)
                    .IsRequired()
                    .HasMaxLength(500);
            });
        }
    }
}
