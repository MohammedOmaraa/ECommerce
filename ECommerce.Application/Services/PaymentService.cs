
using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Baskets;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentGateway paymentGateway;
        private readonly PaymentGatewaySettings _stripe;
        private readonly IMapper mapper;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IPaymentGateway paymentGateway, IOptions<PaymentGatewaySettings> stripeSettings, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.paymentGateway = paymentGateway;
            this._stripe = stripeSettings.Value;
            this.mapper = mapper;
        }

        public async Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(basketId, ct);

            if (basket is null)
                return Result<BasketDto>.Failure(Error.NotFound("Basket Not Found", $"Basket with id {basketId} not found"));

            if(basket.Items.Count == 0)
                return Result<BasketDto>.Failure(Error.Validation("Basket Is Empty", $"Basket with id {basketId} is empty"));

            var productRepo = unitOfWork.GetRepository<Product, int>();

            var productIds = basket.Items.Select(i => i.Id).ToHashSet();

            var products = await productRepo.ListAsync(new ProductWithIdsSpecifications(productIds));

            foreach (var item in basket.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.Id);

                if (product is null) 
                    return Result<BasketDto>.Failure(Error.NotFound("Product Not Found", $"Product with id {item.Id} not found"));

                item.Price = product.Price;
            }

            var deliveryRepo = unitOfWork.GetRepository<DeliveryMethod, int>();
            
            if (basket.DeliveryMethodId is null)
                return Result<BasketDto>.Failure(Error.Validation("DeliveryMethod.Required", "Delivery method is required."));

            var deliveryMethod = await deliveryRepo.GetByIdAsync(basket.DeliveryMethodId.Value, ct);

            if(deliveryMethod is null)
                return Result<BasketDto>.Failure(Error.NotFound("Delivery Method Not Found", $"Delivery method with id {basket.DeliveryMethodId.Value} not found"));

            basket.ShippingPrice = deliveryMethod.Cost;

            var subTotal = basket.Items.Sum(i => i.Quantity * i.Price);

            var amount = (long)Math.Round((subTotal + deliveryMethod.Cost) * 100m, MidpointRounding.AwayFromZero);

            if (!string.IsNullOrEmpty(basket.PaymentIntentId)) 
            {
                var result = await paymentGateway.UpdatePaymentIntentAsync(basket.PaymentIntentId, amount, ct);
                basket.PaymentIntentId = result.PaymentIntentId;
                basket.ClientSecret = result.ClientSecret;
            }
            else
            {
                var result = await paymentGateway.CreatePaymentIntentAsync(amount, _stripe.DefaultCurrency, ct);
                basket.PaymentIntentId = result.PaymentIntentId;
                basket.ClientSecret = result.ClientSecret;
            }

            await basketRepository.CreateOrUpdateBasketAsync(basket, ct:ct);

            return Result<BasketDto>.Success(mapper.Map<BasketDto>(basket));
        }
    }

    public class PaymentGatewaySettings
    {
        public string SecretKey { get; set; } = default!;

        public string DefaultCurrency { get; set; } = "USD";
    }
}
