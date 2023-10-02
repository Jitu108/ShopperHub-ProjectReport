using Catalog.API.Dtos;

namespace Catalog.API.Services
{
    public interface IProductService
    {
        Task<ProductRead> GetProductByIdAsync(long productId);
        Task<IEnumerable<ProductRead>> GetProductByBrandIdAsync(long catalogBrandId);
        Task<IEnumerable<ProductRead>> GetProductByCatalogTypeIdAsync(long catalogtypeId);
        Task<IEnumerable<ProductRead>> GetAllProductsAsync();

        Task<bool> AddProductAsync(ProductCreate productCreateDto);
        Task<bool> UpdateProductAsync(ProductCreate product);
        Task<bool> DeleteProductAsync(long productId);
    }
}