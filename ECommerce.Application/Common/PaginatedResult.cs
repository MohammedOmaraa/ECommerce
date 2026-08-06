
namespace ECommerce.Application.Common
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(IReadOnlyList<TEntity> data, int pageNumber, int pageSize, int count)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
        }

        public IReadOnlyList<TEntity> Data { get; set; } = [];

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }
    }
}
