using Microsoft.EntityFrameworkCore;
using Ordering.API.Data.Entities;
using Ordering.API.Enums;

namespace Ordering.API.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderingDbContext context;

        public OrderRepo(OrderingDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.OrderStatus = OrderStatus.Placed;

            await context.Orders.AddAsync(order);
            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await context.Orders
                .Include(x => x.Items)
                .Include(x => x.DeliveryAddress)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByUserId(int userId)
        {
            return await context.Orders
                .Include(x => x.Items)
                .Include(x => x.DeliveryAddress)
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var order = await context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            order.OrderStatus = orderStatus;
            
        }

        public async Task CancelOrder(CancelledOrder order)
        {
            order.CancellationDate = DateTime.Now;
            await context.CancelledOrders.AddAsync(order);
        }

        public async Task<int> SaveChange()
        {
            return await context.SaveChangesAsync();
        }

        public async Task RefundOrder(RefundedOrder refundedOrder)
        {
            await context.RefundedOrders.AddAsync(refundedOrder);
        }

        public async Task<List<CancelledOrder>> GetCancelledOrders(int userId)
        {
            return await context.CancelledOrders.Include(x => x.Order)
                .Where(x => x.Order.UserId == userId).ToListAsync();
        }

        public async Task<List<RefundedOrder>> GetRefundedOrders(int userId)
        {
            return await context.RefundedOrders.Include(x => x.Order)
                .Where(x => x.Order.UserId == userId).ToListAsync();
        }
    }
}
