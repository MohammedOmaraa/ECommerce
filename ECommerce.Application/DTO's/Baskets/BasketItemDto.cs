
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO_s.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
