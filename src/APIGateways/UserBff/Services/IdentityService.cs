using UserBff.Dtos;
using UserBff.InterServiceCommunication.SyncDataClient;

namespace UserBff.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityClient identityClient;

        public IdentityService(IIdentityClient identityClient)
        {
            this.identityClient = identityClient;
        }
        public Task<bool> AddUser(UserCreate user)
        {
            return identityClient.AddUser(user);
        }

        public Task<AuthUserDto> Authenticate(string email, string password)
        {
            return identityClient.Authenticate(email, password);
        }

        public Task<UserDto> GetUserByEmail(string email)
        {
            return identityClient.GetUserByEmail(email);
        }

        public Task<UserDto> GetUserById(int id)
        {
            return identityClient.GetUserById(id);
        }
    }
}
