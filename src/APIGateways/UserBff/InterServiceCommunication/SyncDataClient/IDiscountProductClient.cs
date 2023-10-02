using UserBff.Dtos;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public interface IDiscountProductClient
    {
        Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update);
        Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync();
    }
}
