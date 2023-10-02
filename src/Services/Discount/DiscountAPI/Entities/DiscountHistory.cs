namespace DiscountAPI.Entities
{
    public class DiscountHistory
    {
        public int Id { get; set; }
        public long ProductExternalId { get; set; }
        public Nullable<decimal> DiscountFlat { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public bool IsDiscountPrencent { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
