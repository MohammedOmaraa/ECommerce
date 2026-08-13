
using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Cost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(d => d.ShortName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(500);

            builder.Property(d => d.DeliveryTime)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);
        }
    }
}
