using AdminBff.Dtos;

namespace AdminBff.Services
{
    public interface ICatalogProductService
    {
        Task<ProductRead> GetProductByIdAsync(long productId);
        Task<IEnumerable<ProductRead>> GetProductByBrandIdAsync(long catalogBrandId);
        Task<IEnumerable<ProductRead>> GetProductByCatalogTypeIdAsync(long catalogtypeId);
        Task<IEnumerable<ProductRead>> GetAllProductsAsync();
        Task<bool> AddProductAsync(ProductCreate product);
        Task<bool> UpdateProductAsync(ProductCreate product);
        Task<bool> DeleteProductAsync(long productId);
        
    }
}
