using Catalog.API.Data.Interface;
using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.SqlDataStore.Repo
{
    public class SqlProductRepo : IProductRepo
    {
        private readonly CatalogDbContext context;

        public SqlProductRepo(CatalogDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);
            if (product.Image != null)
            {
                await context.Images.AddAsync(product.Image);
            }

            var status = await context.SaveChangesAsync();

            return status > 0;
        }

        public async Task<Product> GetProductByIdAsync(long productId)
        {
            var product = await GetProductQueryable()
                .Where(x => x.Id == productId).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByBrandIdAsync(long catalogBrandId)
        {
            return await GetProductQueryable()
            .Where(x => x.CatalogBrandId == catalogBrandId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCatalogTypeIdAsync(long catalogtypeId)
        {
            return await GetProductQueryable()
            .Where(x => x.CatalogTypeId == catalogtypeId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = context.Products.ToList();
            var t = await GetProductQueryable()
            .ToListAsync();
            return t;
        }

        public async Task<bool> IsProductExist(long id)
        {
            return await context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var previousImage = context.Images.Where(x => x.ProductId == product.Id).FirstOrDefault();
            var productInDb = context.Products.Where(x => x.Id == product.Id).First();

            if ((!string.IsNullOrEmpty(product.Image.Name) || !string.IsNullOrEmpty(product.Image.Caption)))
            {
                if (product.Image.Id == 0)
                {
                    if (previousImage != null) context.Images.Remove(previousImage);

                    context.Images.Add(product.Image);
                }
                else
                {
                    previousImage.Caption = product.Image.Caption;
                    previousImage.Data = product.Image.Data;
                }
            }

            productInDb.Name = product.Name;
            productInDb.Description = product.Description;
            productInDb.Price = product.Price;
            productInDb.MRP = product.MRP;
            productInDb.CatalogTypeId = product.CatalogTypeId;
            productInDb.CatalogBrandId = product.CatalogBrandId;
            productInDb.AvailableStock = product.AvailableStock;
            productInDb.RestockThreshold = product.RestockThreshold;
            productInDb.MaxStockThreshold = product.MaxStockThreshold;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var productInDb = await context.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            context.Products.Remove(productInDb);
            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        private IQueryable<Product> GetProductQueryable()
        {
            return context.Products
            .Include(x => x.CatalogBrand)
            .Include(x => x.CatalogType)
            .Include(x => x.Image)
            .AsNoTracking();
        }

        public async Task UpdateProductsPriceAsync(List<Product> products)
        {
            var productIds = products.Select(x => x.Id).ToList();

            var productsInDb = await context.Products.Where(x => productIds.Contains(x.Id)).ToListAsync();

            foreach (var product in productsInDb)
            {
                var incomingProduct = products.First(x => x.Id == product.Id);
                product.Price = incomingProduct.Price;
            }

            var status = await context.SaveChangesAsync();
        }
    }
}
