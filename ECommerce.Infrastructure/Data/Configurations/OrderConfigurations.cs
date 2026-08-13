
using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.BuyerEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.Street).IsRequired().HasMaxLength(100);
                sa.Property(a => a.City).IsRequired().HasMaxLength(100);
                sa.Property(a => a.Country).IsRequired().HasMaxLength(100);
                sa.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
                sa.Property(a => a.LastName).IsRequired().HasMaxLength(50);
            });
        }
    }
}
