
using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using System.Linq.Expressions;

namespace ECommerce.Application.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        protected BaseSpecifications(Expression<Func<TEntity, bool>> Criteria = null)
        {
            this.Criteria = Criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions {get; private set;} = [];

        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }
    }
}
