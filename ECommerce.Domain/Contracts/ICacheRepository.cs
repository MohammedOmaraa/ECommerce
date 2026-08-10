
namespace ECommerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key, CancellationToken ct = default);

        Task SetAsync(string key, string value, TimeSpan timeToLive, CancellationToken ct = default);

    }
}
