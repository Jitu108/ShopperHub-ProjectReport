using UserBff.Dtos;
using UserBff.Enums;
using UserBff.InterServiceCommunication.SyncDataClient;

namespace UserBff.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderClient orderClient;

        public OrderService(IOrderClient orderClient)
        {
            this.orderClient = orderClient;
        }
        public Task<bool> AddOrder(OrderCreate order)
        {
            return orderClient.AddOrder(order);
        }

        public Task<OrderStatusInfo> CancelOrder(CancelOrderDto orderDto)
        {
            return orderClient.CancelOrder(orderDto);
        }

        public Task<List<CancelledOrderDto>> GetCancelledOrders(int userId)
        {
            return orderClient.GetCancelledOrders(userId);
        }

        public Task<OrderDto> GetOrderById(int id)
        {
            return orderClient.GetOrderById(id);
        }

        public Task<IEnumerable<OrderDto>> GetOrdersByUserId(int userId)
        {
            return orderClient.GetOrdersByUserId(userId);
        }

        public Task<List<RefundedOrderDto>> GetRefundedOrders(int userId)
        {
            return orderClient.GetRefundedOrders(userId);
        }

        public Task<OrderStatusInfo> RefundOrder(int orderId)
        {
            return orderClient.RefundOrder(orderId);
        }
    }
}
