namespace Identity.API.Dtos
{
    public class AuthUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
