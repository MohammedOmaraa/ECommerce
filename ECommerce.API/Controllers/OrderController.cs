
using ECommerce.Application.Contracts;
using ECommerce.Application.DTO_s.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderDto orderDto, [FromQuery] string email, CancellationToken ct) 
        {
            return ToActionResult(await orderService.CreateOrderAsync(orderDto, email, ct));
        }

        [HttpGet("delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethods(CancellationToken ct)
        {
            return ToActionResult(await orderService.GetAllDeliveryMethodAsync(ct));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersByEmail([FromQuery] string email,CancellationToken ct)
        {
            return ToActionResult(await orderService.GetAllOrdersByEmailAsync(email, ct));
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAndEmail(Guid id, [FromQuery] string email, CancellationToken ct)
        {
            return ToActionResult(await orderService.GetOrderByIdAndEmailAsync(id, email, ct));
        }
    }
}
