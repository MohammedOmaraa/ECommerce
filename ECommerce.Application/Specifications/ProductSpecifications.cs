
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(int? brandId, int? typeId) 
            : base(p => (!brandId.HasValue || p.BrandId == brandId.Value) && (!typeId.HasValue || p.TypeId == typeId.Value))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
