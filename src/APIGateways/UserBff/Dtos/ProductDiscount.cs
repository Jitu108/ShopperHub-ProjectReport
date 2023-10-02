namespace UserBff.Dtos
{
    public class ProductDiscount
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> DiscountFlat { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public bool IsDiscountPercent { get; set; }
        public decimal MRP { get; set; }
        public decimal Price { get; set; }
    }
}
