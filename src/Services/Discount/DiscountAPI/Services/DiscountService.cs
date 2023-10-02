using AutoMapper;
using DiscountAPI.Data.Interface;
using DiscountAPI.Dtos;
using DiscountAPI.Entities;

namespace DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepo discountRepo;
        private readonly IMapper mapper;

        public DiscountService(IDiscountRepo discountRepo, IMapper mapper)
        {
            this.discountRepo = discountRepo;
            this.mapper = mapper;
        }

        public async Task<bool> AddProductAsync(ProductDiscount product)
        {
            var productModel = mapper.Map<Product>(product);
            return await discountRepo.AddProductAsync(productModel);
        }

        public async Task<bool> UpdateProductAsync(ProductDiscount product)
        {
            var productModel = mapper.Map<Product>(product);
            return await discountRepo.UpdateProductsAsync(productModel);
        }

        public async Task AddProductsAsync(IList<ProductRead> productsRead)
        {
            var products = mapper.Map<List<Product>>(productsRead);

            var productsList = await discountRepo.GetProductsAsync();
            var productIds = productsList.Select(x => x.ProductExternalId).ToList();

            var productsToAdd = new List<Product>();
            var productsToUpdate = new List<Product>();
            foreach (var product in products)
            {
                // If Product does not exist, Add
                if (!productIds.Contains(product.ProductExternalId))
                {
                    product.DiscountFlat = 0;
                    productsToAdd.Add(product);
                }

                // If Product exist, but name or MRP updated, Update
                else
                {
                    var existingProduct = productsList.FirstOrDefault(x => x.ProductExternalId == product.ProductExternalId);
                    if (existingProduct != null
                        && (product.Name != existingProduct.Name || product.MRP != existingProduct.MRP))
                    {
                        productsToUpdate.Add(product);
                    }
                }
            }

            await discountRepo.AddProductsAsync(productsToAdd);
            await discountRepo.UpdateProductsAsync(productsToUpdate);
        }

        public async Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync()
        {
            var products = await discountRepo.GetProductsAsync();
            var productDiscounts = mapper.Map<List<ProductDiscount>>(products);
            return productDiscounts;
        }

        public async Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update)
        {
            return await discountRepo.UpdateDiscountAsync(update.ProductId, update.Discount, update.IsPercent);
        }
    }
}
