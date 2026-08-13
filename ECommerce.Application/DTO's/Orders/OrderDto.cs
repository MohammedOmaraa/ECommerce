
using ECommerce.Application.DTO_s.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO_s.Orders
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; } = default!;

        [Required]
        public int DeliveryMethodId { get; set; }

        [Required]
        public AddressDto ShippingAddress { get; set; } = default!;

    }
}
