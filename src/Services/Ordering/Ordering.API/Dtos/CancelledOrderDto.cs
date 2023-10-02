namespace Ordering.API.Dtos
{
    public class CancelledOrderDto
    {
        public int OrderId { get; set; }
        public DateTime CancellationDate { get; set; }
        public string CancellationReason { get; set; }
    }
}
