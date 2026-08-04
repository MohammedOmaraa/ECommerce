
using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductSpecificationParameters parameters) 
            : base(p => (!parameters.brandId.HasValue || p.BrandId == parameters.brandId.Value)
            && (!parameters.typeId.HasValue || p.TypeId == parameters.typeId.Value)
            && (string.IsNullOrEmpty(parameters.searchValue) || p.Name.ToLower().Contains(parameters.searchValue.ToLower()))
            )
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
