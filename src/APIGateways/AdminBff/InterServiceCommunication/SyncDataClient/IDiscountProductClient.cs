using AdminBff.Dtos;

namespace AdminBff.InterServiceCommunication.SyncDataClient
{
    public interface IDiscountProductClient
    {
        Task<bool> UpdateProductDiscountAsync(DiscountUpdateDto update);
        Task<IEnumerable<ProductDiscount>> GetProductDiscountsAsync();
    }
}
