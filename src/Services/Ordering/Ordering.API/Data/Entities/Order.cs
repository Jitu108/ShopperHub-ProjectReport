using Ordering.API.Enums;

namespace Ordering.API.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public Address DeliveryAddress { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
