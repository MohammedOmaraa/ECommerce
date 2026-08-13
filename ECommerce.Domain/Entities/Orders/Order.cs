
using ECommerce.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Orders
{
    public class Order:BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; } = default!;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public ICollection<OrderItem> Items { get; set; } = [];

        public OrderAddress ShippingAddress { get; set; } = default!;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal Subtotal { get; set; }

        public decimal GetTotal() => Subtotal + (DeliveryMethod?.Cost ?? 0);

    }
}
