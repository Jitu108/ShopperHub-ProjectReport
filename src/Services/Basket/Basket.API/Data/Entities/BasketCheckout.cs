namespace Basket.API.Data.Entities
{
    public class BasketCheckout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}