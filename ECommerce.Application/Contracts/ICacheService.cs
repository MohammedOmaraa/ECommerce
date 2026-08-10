
namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key, CancellationToken ct = default);

        Task SetAsync(string key, object value, TimeSpan timeToLive, CancellationToken ct = default);
    }
}
