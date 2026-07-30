
namespace ECommerce.Domain.Common
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
