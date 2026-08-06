using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Products;
using ECommerce.Application.Params;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var brands = await unitOfWork.GetRepository<ProductsBrand, int>().GetAllAsync(ct);

            var mappedBrands = mapper.Map<IReadOnlyList<ProductsBrand>, IReadOnlyList<BrandDto>>(brands);

            return Result<IReadOnlyList<BrandDto>>.Success(mappedBrands);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductSpecificationParameters parameters, CancellationToken ct = default)
        {
            var specifications = new ProductSpecifications(parameters);

            var products = await unitOfWork.GetRepository<Product, int>().ListAsync(specifications, ct);

            var mappedProducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var countSpecifications = new ProductCountSpecifications(parameters);

            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecifications, ct);

            return Result<PaginatedResult<ProductDto>>.Success(new PaginatedResult<ProductDto>(mappedProducts, parameters.PageNumber, products.Count, totalCount));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var types = await unitOfWork.GetRepository<ProductsType, int>().GetAllAsync(ct);

            var mappedTypes = mapper.Map<IReadOnlyList<ProductsType>, IReadOnlyList<TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Success(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var specifications = new ProductSpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specifications, ct);
            
            if(product == null)
            {
                return Result<ProductDto>.Failure(Error.NotFound("ProductNotFound", $"Product with id {id} not found."));
            }

            var mappedProduct = mapper.Map<Product, ProductDto>(product);

            // Implicit cast to Result<ProductDto> using the Success method
            return mappedProduct;
        }
    }
}
