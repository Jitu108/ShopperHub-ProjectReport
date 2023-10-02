using AutoMapper;
using Catalog.API.Data.Interface;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.InterServiceCommunication.SyncDataClient;
using Catalog.API.RedisConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;
        private readonly IDiscountProductClient discountProduct;
        private readonly IMapper mapper;
        private readonly IDistributedCache cache;

        public ProductService(
            IProductRepo productRepo,
            IDiscountProductClient discountProduct,
            IMapper mapper,
            IDistributedCache distributedCache)
        {
            this.productRepo = productRepo;
            this.discountProduct = discountProduct;
            this.mapper = mapper;
            this.cache = distributedCache;
        }

        public async Task<bool> AddProductAsync(ProductCreate productCreateDto)
        {
            try
            {
                var product = mapper.Map<Product>(productCreateDto);

                var image = mapper.Map<Image>(productCreateDto);
                product.Image = image;

                var status = await productRepo.AddProductAsync(product);
                var productDiscount = mapper.Map<ProductDiscount>(product);
                var status1 = await discountProduct.AddProductAsync(productDiscount);

                return status;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductCreate product)
        {
            var productModel = mapper.Map<Product>(product);
            var image = mapper.Map<Image>(product);
            productModel.Image = image;
            productModel.Id = product.Id;
            
            var isProductExist = await productRepo.IsProductExist(product.Id);
            if (!isProductExist) return false;

            var status = await productRepo.UpdateProductAsync(productModel);

            var productDiscount = mapper.Map<ProductDiscount>(productModel);
            var status1 = await discountProduct.UpdateProductAsync(productDiscount);

            return status;
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var status = await productRepo.DeleteProductAsync(productId);

            return status;
        }
        

        public async Task<IEnumerable<ProductRead>> GetAllProductsAsync()
        {
            var data = await productRepo.GetAllProductsAsync();
            var productDto = mapper.Map<IEnumerable<ProductRead>>(data);

            return productDto;
        }

        public async Task<IEnumerable<ProductRead>> GetProductByBrandIdAsync(long catalogBrandId)
        {
            var data = await productRepo.GetProductByBrandIdAsync(catalogBrandId);
            var productDto = mapper.Map<IEnumerable<ProductRead>>(data);

            return productDto;
        }

        public async Task<IEnumerable<ProductRead>> GetProductByCatalogTypeIdAsync(long catalogtypeId)
        {
            var data = await productRepo.GetProductByCatalogTypeIdAsync(catalogtypeId);
            var productDto = mapper.Map<IEnumerable<ProductRead>>(data);

            return productDto;
        }

        public async Task<ProductRead> GetProductByIdAsync(long productId)
        {
            var data = await productRepo.GetProductByIdAsync(productId);
            var productDto = mapper.Map<ProductRead>(data);

            return productDto;
        }
    }
}