namespace Ordering.API.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
