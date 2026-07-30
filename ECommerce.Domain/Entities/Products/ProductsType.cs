
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductsType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
