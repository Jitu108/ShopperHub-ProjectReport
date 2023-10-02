using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Identity.API.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
