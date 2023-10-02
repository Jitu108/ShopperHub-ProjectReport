
namespace UserBff.Dtos
{
    public class ShoppingCartDto
    {
        public int UserId { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
