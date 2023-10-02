using DiscountAPI.Data.Interface;
using DiscountAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Data.SqlDataStore.Repo
{
    public class SqlDiscountRepo : IDiscountRepo
    {
        private readonly DiscountDbContext context;

        public SqlDiscountRepo(DiscountDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            await context.Products.AddAsync(product);

            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> UpdateProductsAsync(Product product)
        {

            var productInDb = await context.Products.Where(x => x.ProductExternalId == product.ProductExternalId).FirstOrDefaultAsync();

            productInDb.Name = product.Name;
            productInDb.MRP = product.MRP;
            productInDb.DiscountFlat = product.DiscountFlat;
            productInDb.DiscountPercent = product.DiscountPercent;
            productInDb.IsDiscountPercent = product.IsDiscountPercent;

            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> UpdateProductsAsync(List<Product> products)
        {
            var productIds = products.Select(x => x.ProductExternalId).ToList();

            var productsInDb = await context.Products.Where(x => productIds.Contains(x.ProductExternalId)).ToListAsync();

            foreach (var productInDb in productsInDb)
            {
                var productItem = products.First(x => x.ProductExternalId == productInDb.ProductExternalId);

                productInDb.Name = productItem.Name;
                productInDb.MRP = productItem.MRP;
            }

            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> AddProductsAsync(List<Product> products)
        {
            foreach (var product in products)
                await context.Products.AddAsync(product);

            var status = await context.SaveChangesAsync();
            return status > 0;
        }

        public async Task<bool> UpdateDiscountAsync(long productId, decimal discount, bool isPrecent)
        {
            if (isPrecent && discount > 100) discount = 100;

            var product = await context.Products.Where(x => x.ProductExternalId == productId).FirstOrDefaultAsync();
            if (product == null) return false;

            if (isPrecent && product.MRP < discount) discount = product.MRP;

            var discountHistory = new DiscountHistory { ProductExternalId = productId, UpdatedDate = DateTime.Now };

            if (isPrecent)
            {
                product.DiscountPercent = discount;
                product.IsDiscountPercent = true;

                discountHistory.IsDiscountPrencent = true;
                discountHistory.DiscountPercent = discount;
            }
            else
            {
                product.DiscountFlat = discount;
                product.IsDiscountPercent = false;

                discountHistory.IsDiscountPrencent = false;
                discountHistory.DiscountFlat = discount;
            }

            context.DiscountHistories.Add(discountHistory);

            var status = await context.SaveChangesAsync();

            return status > 0;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }
    }
}
