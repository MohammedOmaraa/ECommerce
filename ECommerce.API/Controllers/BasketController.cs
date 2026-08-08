
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Baskets;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct = default)
        {
            var basket = await basketService.GetBasketAsync(id, ct);

            return ToActionResult(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto createBasketDto, CancellationToken ct = default)
        {
            var basket = await basketService.CreateOrUpdateBasketAsync(createBasketDto,TimeSpan.FromDays(7), ct);

            return ToActionResult(basket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct = default)
        {
            var result = await basketService.DeleteBasketAsync(id, ct);

            return ToActionResult(result);
        }
    }
}
