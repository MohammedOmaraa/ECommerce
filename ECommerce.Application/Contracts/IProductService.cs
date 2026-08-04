
using ECommerce.Application.DTO_s.Products;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(int? brandId, int? typeId, CancellationToken ct = default);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default);

        Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default);

        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default);
    }
}
