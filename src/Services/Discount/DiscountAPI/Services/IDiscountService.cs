using DiscountAPI.Dtos;

namespace DiscountAPI.Services
{
    public interface IDiscountService
    {
        Task<bool> AddProductAsync(ProductDiscount product);
        Task<bool> UpdateProductAsync(ProductDiscount product);
        Task AddProductsAsync(IList<ProductRead> products);
        Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update);
        Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync();
    }
}
