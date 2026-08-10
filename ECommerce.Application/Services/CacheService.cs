
using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System.Text.Json;

namespace ECommerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }

        public Task<string?> GetAsync(string key, CancellationToken ct = default)
        {
            return cacheRepository.GetAsync(key, ct);
        }

        public Task SetAsync(string key, object value, TimeSpan timeToLive, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(value, new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return cacheRepository.SetAsync(key, jsonValue, timeToLive, ct);
        }
    }
}
