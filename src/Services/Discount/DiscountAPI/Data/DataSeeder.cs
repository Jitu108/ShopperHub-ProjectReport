using DiscountAPI.InterServiceCommunication.SyncDataClient;

namespace DiscountAPI.Data
{
    public class DataSeeder
    {
        public async Task SeedAsyc(IApplicationBuilder builder)
        {
            await AddCatalogProductAsync(builder);
        }

        public async Task AddCatalogProductAsync(IApplicationBuilder builder)
        {
            using(var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var client = serviceScope.ServiceProvider.GetService<ICatalogProductClient>();
                await client.AddProductsFromCatalogAsync();
            }
        }
    }
}
