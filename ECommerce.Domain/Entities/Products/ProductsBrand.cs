using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductsBrand : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
