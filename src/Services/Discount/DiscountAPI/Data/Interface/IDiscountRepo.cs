using DiscountAPI.Entities;

namespace DiscountAPI.Data.Interface
{
    public interface IDiscountRepo
    {
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductsAsync(Product product);
        Task<bool> AddProductsAsync(List<Product> products);
        Task<bool> UpdateProductsAsync(List<Product> products);
        Task<bool> UpdateDiscountAsync(long productId, decimal discount, bool isPrecent);
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
