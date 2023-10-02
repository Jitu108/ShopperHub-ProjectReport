using UserBff.Dtos;

namespace UserBff.Services
{
    public interface IIdentityService
    {
        Task<AuthUserDto> Authenticate(string email, string password);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
        Task<bool> AddUser(UserCreate user);
    }
}
