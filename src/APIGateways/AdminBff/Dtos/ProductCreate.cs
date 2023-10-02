namespace AdminBff.Dtos
{
    public class ProductCreate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal MRP { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        // Quantity in stock
        public int AvailableStock { get; set; }
        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageCaption { get; set; }
        public string ImageData { get; set; }
    }
}
