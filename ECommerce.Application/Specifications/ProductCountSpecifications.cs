
using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductCountSpecifications:BaseSpecifications<Product,int>
    {
        public ProductCountSpecifications(ProductSpecificationParameters parameters)
            : base(p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
            && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
            && (string.IsNullOrEmpty(parameters.SearchValue) || p.Name.ToLower().Contains(parameters.SearchValue.ToLower()))
            )
        {
            
        }
    }
}
