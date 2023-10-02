using Ordering.API.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.API.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
    }
}
