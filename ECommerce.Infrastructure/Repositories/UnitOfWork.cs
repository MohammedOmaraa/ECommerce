
using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repos = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            if (_repos.TryGetValue(TypeName, out var repo))
            {
                return (IGenericRepository<TEntity, TKey>)repo;
            }

            var NewRepo = new GenericRepository<TEntity, TKey>(dbContext);
            _repos[TypeName] = NewRepo;
            return NewRepo;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await dbContext.SaveChangesAsync(ct);
        }
    }
}
