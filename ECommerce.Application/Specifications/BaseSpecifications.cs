
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

        public Expression<Func<TEntity, object>>? OrderBy {  get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDesc {  get; private set; }

        public int Take { get; private set; }

        public int Skip {  get; private set; }

        public bool IsPaginated { get; private set; }

        public void ApplyPagination(int pagesSize, int pageNumber)
        {
            IsPaginated = true;
            Skip = (pageNumber - 1) * pagesSize;
            Take = pagesSize;
        }

        public void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }

        public void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
            => OrderBy = orderBy;

        public void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDesc)
            => OrderByDesc = orderByDesc;
    }
}
