using UserBff.Dtos;

namespace UserBff.Services
{
    public interface IBasketService
    {
        public Task<ShoppingCartDto> GetBasket(int userId);
        public Task<bool> UpdateBasket(ShoppingCartDto cart);

        public Task<bool> DeleteBasket(int userId);
    }
}
