namespace Ordering.API.Dtos
{
    public class OrderCreate
    {
        public int UserId { get; set; }
        public List<OrderItemCreate> Items { get; set; }
        public AddressDto DeliveryAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMode { get; set; }
    }
}
