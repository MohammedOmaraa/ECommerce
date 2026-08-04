
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications()
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
