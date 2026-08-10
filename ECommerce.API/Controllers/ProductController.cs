using ECommerce.API.Attributes;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Products;
using ECommerce.Application.Params;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class ProductController(IProductService productService) : ApiBaseController
    {
        [HttpGet]
        [RedisCache(43200)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProductsAsync([FromQuery] ProductSpecificationParameters parameters, CancellationToken ct)
        {
            var products = await productService.GetAllProductsAsync(parameters, ct);

            return ToActionResult(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct)
        {
            var brands = await productService.GetAllProductBrandsAsync(ct);

            return ToActionResult(brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct)
        {
            var types = await productService.GetAllProductTypesAsync(ct);

            return ToActionResult(types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct)
        {
            var product = await productService.GetProductByIdAsync(id, ct);

            return ToActionResult(product);
        }
    }
}
