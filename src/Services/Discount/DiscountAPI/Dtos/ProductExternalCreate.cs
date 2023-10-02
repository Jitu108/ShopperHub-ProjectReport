namespace DiscountAPI.Dtos
{
    // Used to Read Product from Catalog
    public class ProductExternalCreate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal MRP { get; set; }
    }
}
