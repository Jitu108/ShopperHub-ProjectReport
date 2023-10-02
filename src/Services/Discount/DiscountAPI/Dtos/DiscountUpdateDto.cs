namespace DiscountAPI.Dtos
{
    public class DiscountUpdateDto
    {
        public long ProductId { get; set; }
        public decimal Discount { get; set; }
        public bool IsPercent { get; set; }

    }
}
