using Basket.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {
        }

        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<ShoppingCartItem> CartItems { get; set; }
        public DbSet<BasketCheckout> Checkout { get; set; }
    }
}
