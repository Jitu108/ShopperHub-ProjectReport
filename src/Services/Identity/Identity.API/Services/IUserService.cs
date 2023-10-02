using Identity.API.Dtos;

namespace Identity.API.Services
{
    public interface IUserService
    {
        Task<AuthUserDto> Authenticate(string email, string password);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
        Task<bool> AddUser(UserCreate user);
    }
}
