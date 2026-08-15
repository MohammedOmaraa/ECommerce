
using ECommerce.Application.DTO_s.Orders;
using static ECommerce.Application.Common.ResultOfT;

namespace ECommerce.Application.Contracts
{
    public interface IOrderService
    {
        Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default);

        Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersByEmailAsync(string email, CancellationToken ct = default);

        Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodAsync(CancellationToken ct = default);

        Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid id, string email, CancellationToken ct = default);

    }
}
