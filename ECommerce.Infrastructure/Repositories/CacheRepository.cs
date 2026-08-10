
using ECommerce.Domain.Contracts;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase database;

        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            this.database = connectionMultiplexer.GetDatabase();
        }

        public async Task<string?> GetAsync(string key, CancellationToken ct = default)
        {
            var value = await database.StringGetAsync(key);

            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string key, string value, TimeSpan timeToLive, CancellationToken ct = default)
        {
            await database.StringSetAsync(key, value, timeToLive);
        }
    }
}
