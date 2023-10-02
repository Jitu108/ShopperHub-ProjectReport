using AdminBff.Dtos;

namespace AdminBff.InterServiceCommunication.SyncDataClient
{
    public interface IIdentityClient
    {
        Task<AuthUserDto> Authenticate(string email, string password);
        Task<UserDto> GetUserById(int id);
        Task<UserDto> GetUserByEmail(string email);
    }
}
