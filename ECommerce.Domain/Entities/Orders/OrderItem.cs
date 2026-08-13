
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Orders
{
    public class OrderItem:BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
