using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Data
{
    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<OrderingDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
