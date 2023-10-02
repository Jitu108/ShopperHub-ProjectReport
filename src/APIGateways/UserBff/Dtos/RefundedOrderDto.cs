namespace UserBff.Dtos
{
    public class RefundedOrderDto
    {
        public int OrderId { get; set; }
        public DateTime RefundDate { get; set; }
        public decimal RefundedAmount { get; set; }
    }
}
