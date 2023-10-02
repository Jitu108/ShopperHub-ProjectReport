namespace UserBff.Dtos
{
    public class CancelOrderDto
    {
        public int OrderId { get; set; }
        public string CancellationReason { get; set; }
    }
}
