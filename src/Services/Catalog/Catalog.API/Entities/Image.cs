namespace Catalog.API.Entities
{
    public class Image
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public byte[] Data { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
