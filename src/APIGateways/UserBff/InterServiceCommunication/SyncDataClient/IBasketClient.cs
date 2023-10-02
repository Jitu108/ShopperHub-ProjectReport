using UserBff.Dtos;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public interface IBasketClient
    {
        public Task<ShoppingCartDto> GetBasket(int userId);
        public Task<bool> UpdateBasket(ShoppingCartDto cart);

        public Task<bool> DeleteBasket(int userId);
    }
}
