
namespace ECommerce.Application.DTO_s.Baskets
{
    public class BasketDto
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
