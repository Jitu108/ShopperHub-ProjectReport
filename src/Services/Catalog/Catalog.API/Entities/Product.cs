namespace Catalog.API.Entities
{
    public class Product : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public decimal MRP { get; set; }
        public long CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }
        public long CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }

        // Quantity in stock
        public int AvailableStock { get; set; }

        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        //public bool OnReorder { get; set; }
        public Image Image { get; set; }

        public Product() { }
    }
}
