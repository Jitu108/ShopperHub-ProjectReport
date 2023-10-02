using UserBff.Dtos;
using UserBff.InterServiceCommunication.SyncDataClient;

namespace UserBff.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketClient basketClient;

        public BasketService(IBasketClient basketClient)
        {
            this.basketClient = basketClient;
        }
        public Task<bool> DeleteBasket(int userId)
        {
            return basketClient.DeleteBasket(userId);
        }

        public Task<ShoppingCartDto> GetBasket(int userId)
        {
            return basketClient.GetBasket(userId);
        }

        public Task<bool> UpdateBasket(ShoppingCartDto cart)
        {
            return basketClient.UpdateBasket(cart);
        }
    }
}
