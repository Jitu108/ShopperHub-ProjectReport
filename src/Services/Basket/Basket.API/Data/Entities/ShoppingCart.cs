using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.API.Data.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart(int userId)
        {
            UserId = userId;
        }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                var totalPrice = 0m;
                foreach (var item in Items)
                {
                    totalPrice += item.UnitPrice * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
