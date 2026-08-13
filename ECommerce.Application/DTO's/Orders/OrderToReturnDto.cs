
using ECommerce.Application.DTO_s.Identity;

namespace ECommerce.Application.DTO_s.Orders
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }

        public string BuyerEmail { get; set; } = default!;

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItemDto> Items { get; set; } = [];

        public AddressDto ShippingAddress { get; set; } = default!;

        public string DeliveryMethod { get; set; } = default!;

        public string Status { get; set; } = default!;

        public decimal Subtotal { get; set; }

        public decimal DeliveryCost { get; set; }

        public decimal Total { get; set; }
    }
}
