namespace UserBff.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDto DeliveryAddress { get; set; }
        public string PaymentMode { get; set; }
        public string OrderStatus { get; set; }
    }
}
