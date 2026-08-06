
using ECommerce.Application.Params;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications(ProductSpecificationParameters parameters) 
            : base(p => (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value)
            && (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
            && (string.IsNullOrEmpty(parameters.SearchValue) || p.Name.ToLower().Contains(parameters.SearchValue.ToLower()))
            )
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch(parameters.Sort)
            {
                case ProductSortBy.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortBy.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortBy.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortBy.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
            }

            ApplyPagination(parameters.PageSize, parameters.PageNumber);
        }

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }
    }
}
