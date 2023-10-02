using UserBff.Dtos;

namespace UserBff.Services
{
    public interface IDiscountProductService
    {
        Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update);
        Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync();
    }
}
