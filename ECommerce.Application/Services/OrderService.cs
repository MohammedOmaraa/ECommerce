
using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Orders;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository) 
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }

        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId, ct);

            if (basket is null)
            {
                return Result<OrderToReturnDto>.Failure(Error.NotFound("Basket.NotFound","Basket is not found"));
            }

            if (basket.Items.Count == 0)
            {
                return Result<OrderToReturnDto>.Failure(Error.Validation("Basket.Empty", "Basket is empty"));
            }

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                return Result<OrderToReturnDto>.Failure(Error.Validation("PaymentIntent.NotFound", "Payment must be initialized before creating the order."));
            }

            var productRepo = unitOfWork.GetRepository<Product, int>();

            var productIds = basket.Items.Select(i=> i.Id).ToHashSet();

            var products = await productRepo.ListAsync(new ProductWithIdsSpecifications(productIds), ct);

            var orderItems = new List<OrderItem>(basket.Items.Count);

            foreach (var item in basket.Items) 
            {
                var product = products.FirstOrDefault(p => p.Id == item.Id);

                if (product is null)
                    return Result<OrderToReturnDto>.Failure(Error.NotFound("Product.NotFound", "Product is not found"));

                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        PictureUrl = product.PictureUrl,
                        ProductName = product.Name,
                    }
                });
            }

            var orderAddress = mapper.Map<OrderAddress>(orderDto.ShippingAddress);

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId, ct);

            if (deliveryMethod is null)
                return Result<OrderToReturnDto>.Failure(Error.NotFound("DeliveryMethod.NotFound", "Delivery Method is not found"));

            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            var order = new Order()
            {
                BuyerEmail = email,
                Items = orderItems,
                ShippingAddress = orderAddress,
                DeliveryMethodId = deliveryMethod.Id,
                Subtotal = subTotal,
                DeliveryMethod = deliveryMethod,
                PaymentIntentId = basket.PaymentIntentId
            };

            unitOfWork.GetRepository<Order, Guid>().Add(order);

            var result = await unitOfWork.SaveChangesAsync();

            if (result <= 0)
                return Result<OrderToReturnDto>.Failure(Error.Failure("Order.Failure", "Order can't created"));

            await basketRepository.DeleteBasketAsync(orderDto.BasketId, ct);

            return Result<OrderToReturnDto>.Success(mapper.Map<OrderToReturnDto>(order));
        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);

            return Result<IReadOnlyList<DeliveryMethodDto>>.Success(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersByEmailAsync(string email, CancellationToken ct = default)
        {
            var orders = await unitOfWork.GetRepository<Order,Guid>().ListAsync(new OrderSpecifications(email), ct);

            return Result<IReadOnlyList<OrderToReturnDto>>.Success(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid id, string email, CancellationToken ct = default)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(new OrderSpecifications(id, email), ct);

            if (order is null)
                return Result<OrderToReturnDto>.Failure(Error.NotFound("Order.notFound", "Order is not found"));

            return Result<OrderToReturnDto>.Success(mapper.Map<OrderToReturnDto>(order));
        }
    }
}
