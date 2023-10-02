using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data
{
    public class DatabaseSeeder
    { 
        public async Task SeedAsync(ShoppingCartDbContext context)
        {
            await context.Database.MigrateAsync();
            await context.SaveChangesAsync();
        }
    }

    
}
