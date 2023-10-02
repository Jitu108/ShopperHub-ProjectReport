using Microsoft.EntityFrameworkCore;

namespace DiscountAPI.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public long ProductExternalId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> DiscountFlat { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public bool IsDiscountPercent { get; set; }
        public decimal MRP { get; set; }
    }
}
