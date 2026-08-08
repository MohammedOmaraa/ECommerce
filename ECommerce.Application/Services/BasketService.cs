
using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Baskets;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, TimeSpan? timeToLive = null, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);

            var result = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, timeToLive, ct);

            return result is not null ? Result<BasketDto>.Success(mapper.Map<BasketDto>(result))
                : Result<BasketDto>.Failure(Error.Failure("CreateOrUpdateBasket.Failure", "Failed to create or update basket."));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await basketRepository.DeleteBasketAsync(basketId, ct);

            return result ? Result<bool>.Success(true) : Result<bool>.Failure(Error.Failure("DeleteBasket.Failure", "Failed to delete basket."));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string basketId, CancellationToken ct = default)
        {
            var result = await basketRepository.GetBasketAsync(basketId, ct);

            return result is not null ? Result<BasketDto>.Success(mapper.Map<BasketDto>(result))
                : Result<BasketDto>.Failure(Error.NotFound("GetBasket.NotFound", "Basket not found."));
        }
    }
}
