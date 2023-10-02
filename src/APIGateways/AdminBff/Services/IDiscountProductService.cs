using AdminBff.Dtos;

namespace AdminBff.Services
{
    public interface IDiscountProductService
    {
        Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update);
        Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync();
    }
}
