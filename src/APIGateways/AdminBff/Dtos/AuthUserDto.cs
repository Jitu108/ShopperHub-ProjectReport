namespace AdminBff.Dtos
{
    public class AuthUserDto : UserDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
