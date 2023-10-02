using UserBff.Dtos;
using UserBff.Enums;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public interface IOrderClient
    {
        Task<bool> AddOrder(OrderCreate order);
        Task<IEnumerable<OrderDto>> GetOrdersByUserId(int userId);
        Task<OrderDto> GetOrderById(int id);

        Task<OrderStatusInfo> CancelOrder(CancelOrderDto orderDto);
        Task<OrderStatusInfo> RefundOrder(int orderId);
        Task<List<CancelledOrderDto>> GetCancelledOrders(int userId);
        Task<List<RefundedOrderDto>> GetRefundedOrders(int userId);
    }
}
