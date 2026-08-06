
namespace ECommerce.Application.Params
{
    public class ProductSpecificationParameters
    {
        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string? SearchValue { get; set; }

        public ProductSortBy Sort { get; set; }

        public int PageNumber { get; set; } = 1;

        private const int DefaultPageSize = 10;

        private const int MaxPageSize = 50;

        private int pageSize = DefaultPageSize;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? DefaultPageSize : value;
        }
    }
}
