using DiscountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Data.SqlDataStore
{
    public class DiscountDbContext : DbContext
    {
        public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<DiscountHistory> DiscountHistories { get; set; }
    }
}
