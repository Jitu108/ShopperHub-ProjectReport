using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data.Entities
{
    public class RefundedOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime RefundDate { get; set; }
        public decimal RefundedAmount { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
