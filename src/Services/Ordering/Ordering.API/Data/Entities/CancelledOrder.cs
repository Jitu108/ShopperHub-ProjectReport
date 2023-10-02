using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data.Entities
{
    public class CancelledOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CancellationDate { get; set; }
        public string CancellationReason { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
