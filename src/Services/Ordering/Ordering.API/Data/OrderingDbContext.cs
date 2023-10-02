using Microsoft.EntityFrameworkCore;
using Ordering.API.Data.Entities;

namespace Ordering.API.Data
{
    public class OrderingDbContext : DbContext
    {
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options)
        {

        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CancelledOrder> CancelledOrders { get; set; }
        public DbSet<RefundedOrder> RefundedOrders { get; set; }
    }
}
