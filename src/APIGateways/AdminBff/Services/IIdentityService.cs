using AdminBff.Dtos;

namespace AdminBff.Services
{
    public interface IIdentityService
    {
        Task<AuthUserDto> Authenticate(string email, string password);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
    }
}
