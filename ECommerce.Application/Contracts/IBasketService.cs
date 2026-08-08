
using ECommerce.Application.DTO_s.Baskets;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default);

        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? timeToLive = null, CancellationToken ct = default);

        Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default);
    }
}
