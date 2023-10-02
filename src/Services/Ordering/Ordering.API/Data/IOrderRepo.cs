using Ordering.API.Data.Entities;
using Ordering.API.Enums;

namespace Ordering.API.Data
{
    public interface IOrderRepo
    {
        Task<bool> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrderByUserId(int userId);
        Task<Order> GetOrderById(int id);
        Task UpdateOrderStatus(int orderId, OrderStatus orderStatus);
        Task CancelOrder(CancelledOrder order);
        Task<int> SaveChange();
        Task RefundOrder(RefundedOrder refundedOrder);
        Task<List<CancelledOrder>> GetCancelledOrders(int userId);
        Task<List<RefundedOrder>> GetRefundedOrders(int userId);
    }
}
