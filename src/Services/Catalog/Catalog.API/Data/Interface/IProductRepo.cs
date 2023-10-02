using Catalog.API.Entities;

namespace Catalog.API.Data.Interface
{
    public interface IProductRepo
    {
        Task<bool> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(long productId);
        Task<IEnumerable<Product>> GetProductByBrandIdAsync(long catalogBrandId);
        Task<IEnumerable<Product>> GetProductByCatalogTypeIdAsync(long catalogtypeId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(long productId);
        Task<bool> IsProductExist(long id);
        Task UpdateProductsPriceAsync(List<Product> products);
    }
}
