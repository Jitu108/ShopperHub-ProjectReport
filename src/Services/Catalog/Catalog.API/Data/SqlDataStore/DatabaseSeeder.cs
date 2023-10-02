using Catalog.API.Entities;

namespace Catalog.API.Data.SqlDataStore
{
    public class DatabaseSeeder
    {
        public async Task SeedAsync(CatalogDbContext context)
        {
            await PopulateTables(context);

        }

        private async Task PopulateTables(CatalogDbContext context)
        {
            //context.Database.Migrate();
            //context.SaveChanges();

            if (!context.CatalogBrands.Any())
            {
                await context.CatalogBrands.AddRangeAsync(new List<CatalogBrand> {
                   //new CatalogBrand {Id = 1, Brand = "Apple"},
                   //new CatalogBrand {Id = 2, Brand = "Samsung"},
                   //new CatalogBrand {Id = 3, Brand = "sony"},
                   //new CatalogBrand {Id = 4, Brand = "Nikon"},
                   //new CatalogBrand {Id = 5, Brand = "Dell"},

                   new CatalogBrand {Brand = "Apple"},
                   new CatalogBrand {Brand = "Samsung"},
                   new CatalogBrand {Brand = "sony"},
                   new CatalogBrand {Brand = "Nikon"},
                   new CatalogBrand {Brand = "Dell"},
                });
                await context.SaveChangesAsync();
            }

            if (!context.CatalogTypes.Any())
            {
                await context.CatalogTypes.AddRangeAsync(new List<CatalogType>{
                    //new CatalogType {Id = 1, Type = "Mobile"},
                    //new CatalogType {Id = 2, Type = "Laptop"},
                    //new CatalogType {Id = 3, Type = "Camera"},
                    //new CatalogType {Id = 4, Type = "TV"}

                    new CatalogType {Type = "Mobile"},
                    new CatalogType {Type = "Laptop"},
                    new CatalogType {Type = "Camera"},
                    new CatalogType {Type = "TV"}
                });
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                var products = new List<Product> {
                    new Product{
                    //Id = 1,
                    Name = "iPhone 13",
                    Description = "iPhone 13",
                    AvailableStock = 3,
                    CatalogBrandId = 1,
                    CatalogTypeId = 1,
                    MaxStockThreshold = 5,
                    RestockThreshold = 1,
                    Price = 20,
                },

                new Product{
                    //Id = 2,
                    Name = "MacBook Air M2",
                    Description = "MacBook Air M2",
                    AvailableStock = 3,
                    CatalogBrandId = 1,
                    CatalogTypeId = 2,
                    MaxStockThreshold = 5,
                    RestockThreshold = 1,
                    Price = 20,
                },

                new Product{
                    //Id = 3,
                    Name = "Nikon D5300",
                    Description = "Nikon D5300",
                    AvailableStock = 3,
                    CatalogBrandId = 4,
                    CatalogTypeId = 3,
                    MaxStockThreshold = 5,
                    RestockThreshold = 1,
                    Price = 20,
                }};

                await context.Products.AddRangeAsync(products);

                await context.SaveChangesAsync();
            }
        }
    }
}
