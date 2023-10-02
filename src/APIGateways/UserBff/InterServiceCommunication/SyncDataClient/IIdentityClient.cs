using UserBff.Dtos;

namespace UserBff.InterServiceCommunication.SyncDataClient
{
    public interface IIdentityClient
    {
        Task<AuthUserDto> Authenticate(string email, string password);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
        Task<bool> AddUser(UserCreate user);
    }
}
