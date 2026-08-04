
using ECommerce.Domain.Common;
using System.Linq.Expressions;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    }
}
