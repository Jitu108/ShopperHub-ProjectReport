using Basket.API.Data.Entities;

namespace Basket.API.Data
{
    public interface IShoppingCartRepo
    {
        public Task<ShoppingCart> GetBasket(int userId);
        public Task<bool> UpdateBasket(ShoppingCart cart);

        public Task<bool> DeleteBasket(int userId);
    }
}
