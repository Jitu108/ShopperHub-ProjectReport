using Microsoft.AspNetCore.Mvc;
using UserBff.Dtos;
using UserBff.Services;

namespace UserBff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("ByUserId/{userId}", Name = "GetOrdersByUserId")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserId(int userId)
        {
            var orders = await orderService.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var order = await orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost("AddOrder", Name = "AddOrder")]
        public async Task<IActionResult> AddOrder(OrderCreate order)
        {
            var orderStatus = await orderService.AddOrder(order);
            return Ok(orderStatus);
        }

        [HttpPost("CancelOrder", Name = "CancelOrder")]
        public async Task<IActionResult> CancelOrder(CancelOrderDto order)
        {
            var orderStatus = await orderService.CancelOrder(order);
            var status = new OrderStatusDto { OrderStatus = orderStatus.ToString() };
            return Ok(status);
        }

        [HttpPost("RefundOrder", Name = "RefundOrder")]
        public async Task<IActionResult> RefundOrder(RefundRequestDto req)
        {
            var orderStatus = await orderService.RefundOrder(req.OrderId);
            var status = new OrderStatusDto { OrderStatus = orderStatus.ToString() };
            return Ok(status);
        }

        [HttpGet("GetCancelledOrders/{userId}", Name = "GetCancelledOrderByUserId")]
        public async Task<ActionResult<IEnumerable<CancelledOrderDto>>> GetCancelledOrderByUserId(int userId)
        {
            var order = await orderService.GetCancelledOrders(userId);
            return Ok(order);
        }

        [HttpGet("GetRefundedOrders/{userId}", Name = "GetRefundedOrderByUserId")]
        public async Task<ActionResult<IEnumerable<RefundedOrderDto>>> GetRefundedOrderByUserId(int userId)
        {
            var order = await orderService.GetRefundedOrders(userId);
            return Ok(order);
        }
    }
}
