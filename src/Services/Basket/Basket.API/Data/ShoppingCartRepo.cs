using Basket.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Basket.API.Data
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly ShoppingCartDbContext context;

        public ShoppingCartRepo(ShoppingCartDbContext context)
        {
            this.context = context;
        }

        public async Task<ShoppingCart> GetBasket(int userId)
        {
            return await context.Carts
            .Include(x => x.Items)
            .Where(x => x.UserId == userId)
            .FirstOrDefaultAsync();
        }

        //public async Task<ShoppingCart> UpdateBasket(int userId, ShoppingCartItem item)
        //{
        //    var basketInRepo = await context.Carts
        //    .Include(x => x.Items)
        //    .Where(x => x.UserId == userId)
        //    .FirstOrDefaultAsync();

        //    // If basket does not exist
        //    if (basketInRepo == null)
        //    {
        //        var basket = new ShoppingCart(userId) { Items = new List<ShoppingCartItem> { item } };
        //        await context.Carts.AddAsync(basket);
        //        await context.SaveChangesAsync();
        //        return basket;
        //    }

        //    // If basket exist 

        //    if (basketInRepo.Items.Any(x => x.ProductId == item.ProductId))
        //    {
        //        // If Item exist
        //        var itemToUpdate = basketInRepo.Items.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
        //        itemToUpdate.Quantity += 1;
        //        context.SaveChanges();
        //    }
        //    else
        //    {
        //        // If item does not exist
        //        //item.Cart = basketInRepo;
        //        basketInRepo.Items.Add(item);
        //        context.SaveChanges();
        //    }

        //    return basketInRepo;
        //}

        public async Task<bool> UpdateBasket(ShoppingCart cart)
        {
            try
            {
                var cartInRepo = await context.Carts.Include(x => x.Items).Where(x => x.UserId == cart.UserId).FirstOrDefaultAsync();
                if (cartInRepo == null)
                {
                    // Add Cart
                    await context.Carts.AddAsync(cart);
                    var status = await context.SaveChangesAsync();
                    return status > 1;
                }
                else
                {
                    var cartItemIds = cartInRepo.Items.Select(x => x.Id).ToList();

                    var cartItems = context.CartItems.Where(x => cartItemIds.Contains(x.Id)).ToList();

                    context.CartItems.RemoveRange(cartItems);
                    //context.SaveChanges();

                    cart.Items.ForEach(x => x.Id = 0);
                    
                    cartInRepo.Items = cart.Items;
                    var status = await context.SaveChangesAsync();
                    return status > 1;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteBasket(int userId)
        {
            try
            {
                var basket = await context.Carts.Include(x => x.Items).Where(x => x.UserId == userId).FirstOrDefaultAsync();
                if (basket != null)
                {
                    context.CartItems.RemoveRange(basket.Items);
                    context.Carts.Remove(basket);
                    var status = context.SaveChanges();
                    return status > 0;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
