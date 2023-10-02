using UserBff.Dtos;
using UserBff.InterServiceCommunication.SyncDataClient;

namespace UserBff.Services
{
    public class CatalogProductService : ICatalogProductService
    {
        private readonly ICatalogProductClient catalogProductClient;

        public CatalogProductService(ICatalogProductClient catalogProductClient)
        {
            this.catalogProductClient = catalogProductClient;
        }

        public Task<IEnumerable<ProductRead>> GetAllProductsAsync()
        {
            return catalogProductClient.GetAllProductsAsync();
        }

        public Task<IEnumerable<ProductRead>> GetProductByBrandIdAsync(long catalogBrandId)
        {
            return catalogProductClient.GetProductByBrandIdAsync(catalogBrandId);
        }

        public Task<IEnumerable<ProductRead>> GetProductByCatalogTypeIdAsync(long catalogtypeId)
        {
            return catalogProductClient.GetProductByCatalogTypeIdAsync(catalogtypeId);
        }

        public Task<ProductRead> GetProductByIdAsync(long productId)
        {
            return catalogProductClient.GetProductByIdAsync(productId);

        }

        public Task<bool> AddProductAsync(ProductCreate product)
        {
            return catalogProductClient.AddProductAsync(product);
        }

        public Task<bool> UpdateProductAsync(ProductCreate product)
        {
            return catalogProductClient.UpdateProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(long productId)
        {
            return catalogProductClient.DeleteProductAsync(productId);
        }
    }
}
